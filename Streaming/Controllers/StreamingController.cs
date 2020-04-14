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
            //var stream = await _streamingService.GetVideoByName(name);
            //return new FileStreamResult(stream, "video/mp4");
            return PhysicalFile(Path.GetFullPath(@"small.mp4"), "application/octet-stream", enableRangeProcessing: true);
        }

        
    }
}