using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using Shouldly;
using Streaming.Controllers;
using Streaming.Infraestructura.Repositories;
using Streaming.Infraestructura.Entities;
using Xunit;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Streaming.Infraestructura.Repositories.contracts;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace StreamingTestUnitarios
{
    public class VideoControllerTest
    {
        [Fact]
        public void TestGetVideosConRepoVacioDaNull()
        {
            var Taskito = Task.Factory.StartNew(() => { return new List<MediaEntity>(); });
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.GetTotalVideos()).Returns(0);
            Repo.Setup(obj => obj.PaginarMedia(0, 0)).Returns( Taskito );
            var controlador = new VideoController(Repo.Object);

            var result = controlador.GetVideos(0);
            result.Wait();
            Assert.Null(result.Result);
        }

        [Fact]
        public void TestGetVideosConRepoLLenoConMenosDeUnaPaginaDevuelveTodos()
        {
            var mockEntity = Mock.Of<MediaEntity>(obj =>
                obj.Id == 1 &&
                obj.Nombre == "Nombre" &&
                obj.Descripcion == "Descripcion" &&
                //obj.Tags == "Tags" &&
                obj.Autor == "Autor"
            );
            var lista = Enumerable.Repeat(mockEntity, 1).ToList();
            var Taskito = Task.Factory.StartNew(() => { return lista; });
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.GetTotalVideos()).Returns(lista.Count);
            Repo.Setup(obj => obj.PaginarMedia(It.IsAny<int>(), It.IsAny<int>())).Returns(Taskito);
            var controlador = new VideoController(Repo.Object);

            var result = controlador.GetVideos(1);
            result.Wait();
            Assert.IsType<PaginadoResponse>(result.Result);
            result.Result.page.ShouldNotBeEmpty();
            result.Result.page.Count().ShouldBe(1);
            var videoresult = result.Result.page[0];
            videoresult.nombre.ShouldBe(mockEntity.Nombre);
            videoresult.descripcion.ShouldBe(mockEntity.Descripcion);
            //videoresult.tags.ShouldBe(mockEntity.Tags);
            videoresult.autor.ShouldBe(mockEntity.Autor);
            videoresult.indice.ShouldBe(mockEntity.Id.ToString());
        }
        //ElPaginadoCorrespondiente

        [Fact]
        public void TestGetVideoPorIdQueNoExisteDaResultadoVacio()
        {
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.getMediaById(It.IsAny<string>())).Throws<InvalidOperationException>();
            var controlador = new VideoController(Repo.Object);

            var result = controlador.getFileById(It.IsAny<string>());
            result.ShouldBeOfType<EmptyResult>();
        }

        private string buscarpelicula()
        {
            var testingDirec = Directory.GetParent(System.IO.Directory.GetCurrentDirectory());
            string solutionDirec = testingDirec.Parent.Parent.Parent.FullName;
            string directorio = Path.Combine(solutionDirec, "Streaming\\StreamingMovies\\");
            return directorio;
        }
        [Fact]
        public void TestGetVideoPorIdEntregaUnArchivo()
        {
            string ruta = buscarpelicula() + "1280.mp4";
            var fileStream = File.Open( ruta, System.IO.FileMode.Open);
            /*
            var fixture = new Fixture();
            fixture.Customizations.Add( new PropertyTypeOmitter(typeof(FileStreamResult)));
            fixture //.Customize(new FileStreamResultCustom())
                   .Customize(new AutoMoqCustomization())
                   .Behaviors.Add(new OmitOnRecursionBehavior());
            //var fileStream = fixture.Create<FileStreamResult>() as FileStreamResult; //Lanza excepcion:
            //AutoFixture.ObjectCreationExceptionWithPath : AutoFixture was unable to create an instance from Microsoft.AspNetCore.Mvc.FileStreamResult because creation unexpectedly failed with exception.
            //Please refer to the inner exception to investigate the root cause of the failure.
            */
            try
            {
                var mockEntity = Mock.Of<MediaEntity>(obj =>
                    obj.Ruta == "Streaming\\StreamingMovies\\1280.mp4"
                );
                var Repo = new Mock<IStreamRepository>();
                var controlador = new VideoController(Repo.Object);
                Repo.Setup(obj => obj.GetFile(It.IsAny<string>(), controlador))
                    .Returns(controlador.File(fileStream, "application/octet-stream"));
                Repo.Setup(obj => obj.getMediaById(It.IsAny<string>())).Returns(mockEntity);

                var result = controlador.getFileById(It.IsAny<string>());
                result.ShouldBeOfType<FileStreamResult>();
                (result as FileStreamResult).FileStream.ShouldBe(fileStream);
                //Etc
            }
            catch(Exception e)
            {
                Assert.True(false);
            }
            finally
            {
                fileStream.Close();
            }
        }

        [Fact]
        public void TestGetSearchVideosDeAlgoQueNoExisteDaVacio()
        {
            var mockEntityHorror = Mock.Of<MediaEntity>(obj =>
                obj.Id == It.IsAny<int>() &&
                obj.Nombre == "Horror"
            );
            var TaskComedia = Task.Factory.StartNew(() => new List<MediaEntity>());
            var TaskHorror = Task.Factory.StartNew(() => Enumerable.Repeat(mockEntityHorror, 2).ToList());
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.SearchVideos("Comedia")).Returns(TaskComedia);
            Repo.Setup(obj => obj.SearchVideos("Horror")).Returns(TaskHorror);
            var controlador = new VideoController(Repo.Object);

            var result = controlador.getSearchVideos("Comedia");
            result.Wait();
            result.Result.ShouldBeEmpty();
        }

        [Fact]
        public void TestGetSearchVideosDeComediaDevuelveTodoEseGenero()
        {
            var mockEntityComedia = Mock.Of<MediaEntity>(obj =>
                obj.Id == It.IsAny<int>() &&
                obj.Nombre == "Comedia"
            );
            var mockEntityHorror = Mock.Of<MediaEntity>(obj =>
                obj.Id == It.IsAny<int>() &&
                obj.Nombre == "Horror"
            );
            var TaskComedia = Task.Factory.StartNew(() => Enumerable.Repeat(mockEntityComedia, 2).ToList() );
            var TaskHorror = Task.Factory.StartNew(() => Enumerable.Repeat(mockEntityHorror, 2).ToList());
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.SearchVideos("Comedia")).Returns(TaskComedia);
            Repo.Setup(obj => obj.SearchVideos("Horror")).Returns(TaskHorror);
            var controlador = new VideoController(Repo.Object);

            var result = controlador.getSearchVideos("Comedia");
            result.Wait();
            result.Result.ShouldNotBeEmpty();
            result.Result.Count().ShouldBe(2);
            result.Result.ShouldAllBe(videores => videores.nombre == ("Comedia"));
        }

        [Fact]
        public void TestSugerenciaTraeLoQueHallaEnRepo()
        {
            var LasSugerencias = It.IsAny<List<string>>();
            var TaskSug = Task.Factory.StartNew(() => LasSugerencias);
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.GetSugerencias("Comedia")).Returns(TaskSug);
            var controlador = new VideoController(Repo.Object);

            var result = controlador.GetSugerencias("Comedia");
            result.Wait();
            result.Result.ShouldBeSameAs(LasSugerencias);
        }

    internal class PropertyTypeOmitter : AutoFixture.Kernel.ISpecimenBuilder
    {
        private readonly Type type;

        internal PropertyTypeOmitter(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            this.type = type;
        }

        internal Type Type
        {
            get { return this.type; }
        }

        public object Create(object request, AutoFixture.Kernel.ISpecimenContext context)
        {
            var propInfo = request as System.Reflection.PropertyInfo;
            if (propInfo != null && propInfo.PropertyType == type)
                return new AutoFixture.Kernel.OmitSpecimen();

            return new AutoFixture.Kernel.NoSpecimen();
        }
    }
        }

    /*Fuente http://www.uliasz.com/2018/03/07/autofixture-customization-for-asp-net-core-web-api-controller/
    public class FileStreamResultCustom : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() => new CustomCompositeMetadataDetailsProvider());
            fixture.Inject(new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(fixture.Create(), fixture.Create()));
        }

        private class CustomCompositeMetadataDetailsProvider : Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.ICompositeMetadataDetailsProvider
        {
            public void CreateBindingMetadata(BindingMetadataProviderContext context)
            {
                throw new System.NotImplementedException();
            }

            public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
            {
                throw new System.NotImplementedException();
            }

            public void CreateValidationMetadata(ValidationMetadataProviderContext context)
            {
                throw new System.NotImplementedException();
            }
        }
    }*/
}
