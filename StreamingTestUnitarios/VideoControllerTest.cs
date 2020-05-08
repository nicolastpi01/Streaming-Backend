using System;
using Moq;
using Streaming.Controllers;
using Streaming.Infraestructura;
using Xunit;

namespace StreamingTestUnitarios
{
    public class VideoControllerTest
    {
        [Fact]
        public void Test1()
        {
            var mediaC = new Mock<MediaContext>();
            var controlador = new VideoController(mediaC.Object);
        }
    }
}
