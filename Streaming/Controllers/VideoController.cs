using System;
using System.Threading.Tasks;
using S = System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private System.Collections.ArrayList listaVideos;
        public static string downloadFilePath = @"C:\Users\NICO\source\repos\Streaming\Streaming\small.mp4";

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
            //string valor = @listaVideos[fileId] as string;
            string path = Path.GetFullPath("small.mp4");

            //IFileProvider provider = new PhysicalFileProvider(BASE_PATH);
            //var fileInfo = provider.GetFileInfo(path);
            //var fileStream = fileInfo.CreateReadStream();

            var file2 = System.IO.File.Open(downloadFilePath, FileMode.Open);
            
            //return PhysicalFile(path, "application/octet-stream", enableRangeProcessing: true);
            return File(file2, "application/octet-stream");
        }

    }

}