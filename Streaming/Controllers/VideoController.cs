using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Streaming.Infraestructura;
using NuGet.Protocol;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private MediaContext Context;

        public VideoController(MediaContext contexto)
        {
            Context = contexto;
        }

        [HttpGet]
        [Route("getFileById")]
        public ActionResult getFileById(string fileId)
        {
            try
            {
                var ruta = Context.Medias
                    .Where(media => media.Id.ToString().Equals(fileId))
                    .Select(media => media.Ruta)
                    .Single();

                string path = Path.GetFullPath(ruta);
                var fileStream = System.IO.File.Open(path, FileMode.Open);

                return File(fileStream, "application/octet-stream");
            }
            catch(InvalidOperationException)
            {
                return new EmptyResult();
            }
        }


        [HttpGet]
        [Route("videos")]
        public Task<List<VideosResult>> GetVideos()
        {
            return Context.Medias
                .Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre))  //esto es el map, viene por extension de LINQ
                .ToListAsync();
        }

        [HttpGet]
        [Route("sugerencias")]
        public Task<List<string>> GetSugerencias(string sugerencia) // las sugerencias para una posible busqueda
        {
            return Context.Medias
                .Select(pair => pair.Nombre) //esto es el map, viene por extension de LINQ; solo busca por nombre, deberia buscar por mas cosas. (categoria, popularidad, etc)
                .Where(pair => pair.Contains(sugerencia)).Take(10) // el condicional es mas grande, toma las primeras 10 sugerencias
                .ToListAsync();
        }


        [HttpGet]
        [Route("cargar")]
        public IActionResult GuargarVideo()
        {
            Context.Medias
                .Add(new Infraestructura.Entities.MediaEntity{ Nombre = "a", Ruta = "b" });
            Context.SaveChanges();
            return Ok();
        }

        
  
        //La busqueda de videos a partir de una busqueda string
        [HttpGet]
        [Route("search")]
        public Task<List<VideosResult>> getSearchVideos(string busqueda)
        {
            return Context.Medias
                .Where(pair => pair.Nombre.Contains(busqueda)) // solo busca por nombre, deberia buscar por mas cosas. (categoria, popularidad, etc)
                .Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre))  //esto es el map, viene por extension de LINQ
                .ToListAsync();
        } 
    }

    public class VideosResult
    {
        public string indice { get; set; }
        public string nombre { get; set; }

        public VideosResult(string indice, string nombre)
        {
            this.indice = indice;
            this.nombre = nombre;
        }
    }

}
 