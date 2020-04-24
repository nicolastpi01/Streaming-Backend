using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Streaming.Domain;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;
using System.Linq;
using System;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private MediaRepo repo;
        private MediaContext Context;

        public VideoController(IConfiguration Configuration, MediaContext contexto)
        {
            repo = new MediaRepo();
            Context = contexto;
            /*
            repo.Add("video1", Configuration["video1"]);
            repo.Add("video2", Configuration["video2"]);
            repo.Add("video3", Configuration["video3"]);*/
        }

        [HttpGet]
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
            return Context.Media
                .Select(pair => new VideosResult( pair.Id.ToString(), pair.Nombre))  //esto es el map, viene por extension de LINQ
                .ToJson();
            /*
            return repo.listaVideos
                .Select(pair => new VideosResult(pair.Key, pair.Value.Nombre))  //esto es el map, viene por extension de LINQ
                .ToJson();*/
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
        public string indice;
        public string nombre;

        public VideosResult(string indice, string nombre)
        {
            this.indice = indice;
            this.nombre = nombre;
        }
    }

}