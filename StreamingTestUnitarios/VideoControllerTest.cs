using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Moq;
using Streaming.Controllers;
using Streaming.Infraestructura;
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
            return dbSet.Object;
        }

        [Fact]
        public void Get_ShouldReturn_HttpStatusOk()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            var sutMediaC = fixture.Freeze<MediaContext>();

            var data = new List<MediaEntity>
            {
                new MediaEntity { }
            }.AsQueryable();

            sutMediaC.Medias = BuildDbSet(data);
            var controlador = new VideoController(sutMediaC);

            controlador.GetVideos(1).Wait();
        }

    }
}
