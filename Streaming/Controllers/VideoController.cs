using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private System.Collections.ArrayList listaVideos;

        public VideoController(IConfiguration Configuration)
        {
            listaVideos = new System.Collections.ArrayList();
            listaVideos.Add(Configuration["video1"]);
            listaVideos.Add(Configuration["video2"]);
            listaVideos.Add(Configuration["video3"]);
        }

        [Route("getFileById")]
        public FileResult getFileById(int fileId)
        {
            fileId = fileId < 0 ? 0 : fileId;
            fileId = fileId > 2 ? 2 : fileId;
     
            string path = Path.GetFullPath(@listaVideos[fileId] as string);
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            
            return File(fileStream, "application/octet-stream");
        }

    }

}