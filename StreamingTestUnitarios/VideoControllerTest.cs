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

namespace StreamingTestUnitarios
{
    public class VideoControllerTest
    {
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
        public void GetVideosConRepoVacioDaNull()
        {
            var Repo = new Mock<StreamRepository>();

            var controlador = new VideoController(Repo.Object);

            var result = controlador.GetVideos(1);
            result.Wait();
            Assert.Null(result.Result);
            /*
            Assert.IsType<PaginadoResponse>(result.Result);
            result.Result.page.ShouldBeEmpty();
            result.Result.offset.ShouldBe(10);
            result.Result.size.ShouldBe(0);*/
        }

        [Fact]
        public void GetVideosConRepoLLenoConMenosDeUnaPaginaDevuelveTodos()
        {
            var Repo = new Mock<StreamRepository>();

            var controlador = new VideoController(Repo.Object);


            /*
            var result = controlador.GetVideos(1);
            result.Wait();
            result.Result.page.ShouldNotBeEmpty();*/
            //Assert.IsType<PaginadoResponse>(result.Result);
        }
        //ElPaginadoCorrespondiente
    }
}
