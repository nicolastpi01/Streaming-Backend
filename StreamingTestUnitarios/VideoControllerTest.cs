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
                obj.Nombre == "A"
            );
            var lista = Enumerable.Repeat(mockEntity, 2).ToList();
            var Taskito = Task.Factory.StartNew(() => { return lista; });
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.GetTotalVideos()).Returns(lista.Count);
            Repo.Setup(obj => obj.PaginarMedia(It.IsAny<int>(), It.IsAny<int>())).Returns(Taskito);
            var controlador = new VideoController(Repo.Object);

            var result = controlador.GetVideos(1);
            result.Wait();
            Assert.IsType<PaginadoResponse>(result.Result);
            result.Result.page.ShouldNotBeEmpty();
            result.Result.page.Count().ShouldBe(2);
        }
        //ElPaginadoCorrespondiente

        [Fact]
        public void TestGetVideoPorIdQueNoExisteDaResultadoVacio()
        {
            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.getMediaById(It.IsAny<string>())).Throws<InvalidOperationException>();
            var controlador = new VideoController(Repo.Object);

            var result = controlador.getFileById(It.IsAny<string>());
            result.ShouldBeOfType<Microsoft.AspNetCore.Mvc.EmptyResult>();
        }

        //[Fact]
        public void TestGetVideoPorIdEntregaUnArchivo()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            //System.IO.File basket = fixture.Freeze<System.IO.File>();

            var Repo = new Mock<IStreamRepository>();
            Repo.Setup(obj => obj.getMediaById(It.IsAny<string>()).Ruta).Returns("");
            var controlador = new VideoController(Repo.Object);

            var result = controlador.getFileById(It.IsAny<string>());
            result.ShouldBeOfType<Microsoft.AspNetCore.Mvc.FileStreamResult>();
            //Etc
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
    }
}
