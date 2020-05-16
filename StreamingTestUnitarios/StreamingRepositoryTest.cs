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
            var entidadUnica = new MediaEntity("Nombre", "Ruta", "Descripcion", "Autor"); 


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
            var entidadUnica = new MediaEntity("Nombre", "Ruta", "Descripcion", "Autor");

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
    }
}
