using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using Shouldly;
using Streaming.Controllers;
using Streaming.Infraestructura;
using Streaming.Infraestructura.Entities;
using Streaming.Infraestructura.Repositories;
using Xunit;

namespace StreamingTestUnitarios
{
    public class StreamingRepositoryTest
    {
        
        [Fact]
        public async void TestPaginarMediaSinVideosDaListaVacia()
        {
            using (var dbContexto = DataContextGenerator.Generate())
            {
                var repo = new StreamRepository(dbContexto);

                var result = await repo.PaginarMedia(0,10);
                result.ShouldBeEmpty();
            }
        }

        [Fact]
        public async void TestPaginarMediaConVideoDaListaConEseElemento()
        {
            var entidadUnica = new MediaEntity("Nombre", "Ruta", "Descripcion", "Autor", "Imagen"); 

            using (var dbContexto = DataContextGenerator.Generate())
            {
                dbContexto.Medias.Add(entidadUnica);
                dbContexto.SaveChanges();

                var repo = new StreamRepository(dbContexto);

                var result = await repo.PaginarMedia(0, 10);
                result.Count.ShouldBe(1);
                Assert.Same(entidadUnica, result[0]);
            }
        }

        [Fact]
        public void GetVideosPorIdConContextoVacioLanzaExcepcion()
        {
            using (var dbContexto = DataContextGenerator.Generate())
            {
                var repo = new StreamRepository(dbContexto);
                Assert.Throws<InvalidOperationException>(() => repo.getMediaById(It.IsAny<string>()) );
            }

        }

        [Fact]
        public void GetVideosPorIdConContexto1ElemLoDevuelve()
        {

            var entidadUnica = new MediaEntity("Nombre", "Ruta", "Descripcion", "Autor", "Imagen");

            using (var dbContexto = DataContextGenerator.Generate())
            {
                dbContexto.Medias.Add(entidadUnica);
                dbContexto.SaveChanges();
                var Idnueva = dbContexto.Medias.Where(x=>true).ToList().Single().Id;

                var repo = new StreamRepository(dbContexto);
                var result = repo.getMediaById(Idnueva.ToString());
                result.Autor.ShouldBeSameAs(entidadUnica.Autor);
                result.Descripcion.ShouldBeSameAs(entidadUnica.Descripcion);
                result.Nombre.ShouldBeSameAs(entidadUnica.Nombre);
                result.Ruta.ShouldBeSameAs(entidadUnica.Ruta);
                result.Tags.ShouldBeSameAs(entidadUnica.Tags);
            }

        }

        [Fact]
        public async void TestGuardarAchivoQueNoEsAceptadoDevuelveStringNulo()
        {
            var fileMock = new Mock<IFormFile>();

            var fileName = It.IsAny<string>();
            var fileExtension = It.IsNotIn<string>( new List<string>{ ".mp4", ".png" });
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("BlockBinario");
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName + fileExtension);
            fileMock.Setup(_ => _.ContentType).Returns("");
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            using (var dbContexto = DataContextGenerator.Generate())
            {
                var repo = new StreamRepository(dbContexto);
                (await repo.SaveVideo(fileMock.Object,fileName)).ShouldBe("");
            }
        }

        [Fact]
        public async void TestGuardarAchivoQueEsAceptadoPeroEstaVacioDevuelveStringNulo()
        {
            var fileMock = new Mock<IFormFile>();

            var fileName = It.IsAny<string>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            //Se omite el writer.Write
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName + ".any");
            fileMock.Setup(_ => _.Length).Returns(0);

            using (var dbContexto = DataContextGenerator.Generate())
            {
                var repo = new StreamRepository(dbContexto);
                fileMock.Setup(_ => _.ContentType).Returns("video");
                (await repo.SaveVideo(fileMock.Object, fileName)).ShouldBe("");
                fileMock.Setup(_ => _.ContentType).Returns("image");
                (await repo.SaveVideo(fileMock.Object, fileName)).ShouldBe("");
            }
        }

        [Fact]
        public async void TestGuardarAchivoQueEsVideoDevuelveStringRuta()
        {
            var fileMock = new Mock<IFormFile>();

            var fileName = "TestGuardarAchivoQueEsVideoDevuelveStringRuta";
            var fileExtension = ".mp4";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("BlockBinario");
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName + fileExtension);
            fileMock.Setup(_ => _.ContentType).Returns("video");
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            using (var dbContexto = DataContextGenerator.Generate())
            {
                var repo = new StreamRepository(dbContexto);
                var result = (await repo.SaveVideo(fileMock.Object, fileName));
                result.ShouldNotBe("");
                result.EndsWith(fileName + fileExtension);
                File.Exists(result).ShouldBeTrue();
                File.Delete(result);
            }
        }

        [Fact]
        public async void TestGuardarAchivoQueEsImagenDevuelveStringRuta()
        {
            var fileMock = new Mock<IFormFile>();

            var fileName = "TestGuardarAchivoQueEsImagenDevuelveStringRuta";
            var fileExtension = ".png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("BlockBinario");
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName + fileExtension);
            fileMock.Setup(_ => _.ContentType).Returns("image");
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            using (var dbContexto = DataContextGenerator.Generate())
            {
                var repo = new StreamRepository(dbContexto);
                var result = (await repo.SaveVideo(fileMock.Object, fileName));
                result.ShouldNotBe("");
                result.EndsWith(fileName + fileExtension);
                File.Exists(result).ShouldBeTrue();
                File.Delete(result);
            }
        }
    }
}
