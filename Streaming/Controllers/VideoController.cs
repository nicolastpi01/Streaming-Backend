using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Streaming.Domain;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            
        }

        [HttpGet]
        [Route("sugerencias")]
        public Task<List<string>> GetSugerencias(string sugerencia)
        {
            return Context.Media
                .Select(pair => pair.Nombre) //esto es el map, viene por extension de LINQ
                .Where(pair => pair.Contains(sugerencia)) // el condicional es mas grande
                .ToListAsync();
        }

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
 