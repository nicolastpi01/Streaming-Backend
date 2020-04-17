using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Streaming.Domain;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private MediaRepo repo;

        public VideoController(IConfiguration Configuration)
        {
            repo = new MediaRepo();
            repo.Add("video1", Configuration["video1"]);
            repo.Add("video2", Configuration["video2"]);
            repo.Add("video3", Configuration["video3"]);
        }

        [Route("getFileById")]
        public FileResult getFileById(int fileId)
        {
            fileId = fileId < 0 ? 0 : fileId;
            fileId = fileId > 3 ? 3 : fileId;
     
            string path = Path.GetFullPath(repo.ruta(fileId));
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            
            return File(fileStream, "application/octet-stream");
        }

        
         [HttpGet]
         [Route("videos")]
         public string GetVideos()
         {
            System.Collections.ArrayList videos = new System.Collections.ArrayList();
            int index = 0;
            foreach (var video in repo.listaVideos)
            {
                videos.Add( new VideosResult(index++,(video as Media).Nombre) );
            }

            return videos.ToJson();
         }
        /*
        [HttpGet]
        [Route("videos")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
    }

    class VideosResult
    {
        public int indice;
        public string nombre;

        public VideosResult(int indice, string nombre)
        {
            this.indice = indice;
            this.nombre = nombre;
        }
    }

}