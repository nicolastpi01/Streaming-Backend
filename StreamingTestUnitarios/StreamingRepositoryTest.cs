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
using Xunit;

namespace StreamingTestUnitarios
{
    public class StreamingRepositoryTest
    {
        /*
        public DbSet<MediaEntity> BuildDbSet(IQueryable<MediaEntity> data)
        {
            var dbSet = new Mock<DbSet<MediaEntity>>();

            dbSet.As<IQueryable<MediaEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<MediaEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<MediaEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<MediaEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            //dbSet.As<IQueryable<MediaEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator() );
            return dbSet.Object;
        }

        [Fact]
        public void GetVideosConContextoVacioDaNull()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var sutMediaC = fixture.Freeze<MediaContext>();

            var data = new List<MediaEntity>().AsQueryable();
            sutMediaC.Medias = BuildDbSet(data);
            var controlador = new VideoController(sutMediaC);

            var result = controlador.GetVideos(1);
            result.Wait();
            Assert.Null(result.Result);*/

        //---------------------------
            /*
            Assert.IsType<PaginadoResponse>(result.Result);
            result.Result.page.ShouldBeEmpty();
            result.Result.offset.ShouldBe(10);
            result.Result.size.ShouldBe(0);*/

        /*
        }

        [Fact]
        public async void GetVideosConContextoLLenoConMenosDeUnaPaginaDevuelveTodos()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var sutMediaC = fixture.Freeze<MediaContext>();

            var mockEntity = Mock.Of<MediaEntity>(obj =>
                obj.Id == 0 &&
                obj.Nombre == "A"
            );
            
            var data = Enumerable.Repeat(mockEntity, 11).ToList().AsQueryable();

            sutMediaC.Medias = BuildDbSet(data);
            var controlador = new VideoController(sutMediaC);
            var l = sutMediaC.Medias
                                    .Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre))
                                    .Skip(0)
                                    .Take(10)
                                    .ToListAsync();
            (await l).ShouldNotBeEmpty();
        */
            /*
            var result = controlador.GetVideos(1);
            result.Wait();
            result.Result.page.ShouldNotBeEmpty();*/
            //Assert.IsType<PaginadoResponse>(result.Result);
        //}

        
        //metodo ElPaginadoCorrespondiente
    }
}
