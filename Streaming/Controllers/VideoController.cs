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
            contexto.ApplyConfiguration(Configuration);
            Context = contexto;

        }

        [HttpGet]
        [Route("getFileById")]
        public ActionResult getFileById(string fileId)
        {
            try
            {
                var ruta = Context.Media
                    .Where(media => media.Id.ToString().Equals(fileId))
                    .Select(media => media.Ruta)
                    .Single();

                string path = Path.GetFullPath(ruta);
                var fileStream = System.IO.File.Open(path, FileMode.Open);

                return File(fileStream, "application/octet-stream");
            }
            catch(InvalidOperationException e)
            {
                return new EmptyResult();
            }
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