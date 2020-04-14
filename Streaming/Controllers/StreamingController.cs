using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streaming.Services;

namespace Streaming.Controllers
{
    /*
    [Route("api/[controller]")]
    [ApiController]
    public class StreamingController : ControllerBase
    {
        private IAzureVideoStreamService _streamingService;

        public StreamingController(IAzureVideoStreamService streamingService)
        {
            _streamingService = streamingService;
        }

        [HttpGet]
        public FileResult Get()
        {

            return PhysicalFile(Path.GetFullPath(@"KONO DIO DA.mp4"), "application/octet-stream", enableRangeProcessing: true);
        }

       
        [HttpGet]
        [Route("buscar/{video}")]
        public Task<Stream> GetVideo(string video)
        {
            return _streamingService.GetVideoByName(video);
        }
            
            [HttpGet]
            [Route("[action]")]
            public IActionResult GetVideo(int id)
            {
                var fileName = GetVideoFileName(id);
                var video = new VideoStream(fileName);
                var response = new HttpResponseMessage
                {
                    Content = new PushStreamContent(video.WriteToStream, new MediaTypeHeaderValue("video/mp4"))
                };
                var objectResult = new ObjectResult(response);
                objectResult.ContentTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("video/mp4"));
                return objectResult;
            }

        } */
}