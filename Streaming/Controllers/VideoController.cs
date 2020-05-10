using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Streaming.Infraestructura;
using Microsoft.AspNetCore.Routing;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private MediaContext Context;
        private int offset;

        public VideoController(MediaContext contexto)
        {
            Context = contexto;
            offset = 10;
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
            catch (InvalidOperationException)
            {
                return new EmptyResult();
            }
        }


        [HttpGet]
        [Route("videos")]
        public async Task<PaginadoResponse> GetVideos(int page)
        {
            var total = Context.Medias.Count();

            var indice = page * offset;

            try
            {
                var resultado = await Context.Medias
                                    .Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre, pair.Descripcion, pair.Tags, pair.Autor))
                                    .Skip(indice)
                                    .Take(offset)
                                    .ToListAsync();

                return new PaginadoResponse(offset, total, resultado);
            }
            catch (Exception e)
            {
                var i = e.Message;
            }
            return null;
        }

        //La busqueda de videos a partir de una busqueda string. Requiere paginado
        [HttpGet]
        [Route("search")]
        public Task<List<VideosResult>> getSearchVideos(string busqueda)
        {
            return Context.Medias
                .Where(pair => pair.Nombre.Contains(busqueda) || pair.Autor.Contains(busqueda) || pair.Descripcion.Contains(busqueda) || pair.Tags.Contains(busqueda)) // solo busca por nombre, deberia buscar por mas cosas. (categoria, popularidad, etc)
                .Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre, pair.Descripcion, pair.Tags, pair.Autor))  //esto es el map, viene por extension de LINQ
                .ToListAsync();
        }

        [HttpGet]
        [Route("sugerencias")]
        public Task<List<string>> GetSugerencias(string sugerencia) // las sugerencias para una posible busqueda
        {
            IQueryable<string> result;
            string lower = "";
            if (sugerencia != null) lower = sugerencia.ToLower();
            result = Context.Medias
                            .Where(pair => pair.Nombre.ToLower().Contains(lower)) 
                            .Select(pair => pair.Nombre);

            if (result.Count() > 0) return result.Take(10).ToListAsync();

            result = Context.Medias
                           .Where(pair => pair.Autor.ToLower().Contains(lower))
                           .Select(pair => pair.Autor);
            if (result.Count() > 0) return result.Take(1).ToListAsync();

            result = Context.Medias /* revisar que sepa separar por comas los tags en las sugerencias oo crear otra tabla para los tags */
                           .Where(pair => pair.Tags.ToLower().Contains(lower))
                           .Select(pair => pair.Tags);
            if (result.Count() > 0) return result.Take(1).ToListAsync();

                    Context.Medias /* revisar este ultimo, se puede hacer algo mejor */
                           .Where(pair => pair.Descripcion.ToLower().Contains(lower))
                           .Select(pair => pair.Descripcion);
                            return result.Take(1).ToListAsync();

        }


        [HttpGet]
        [Route("cargar")]
        public IActionResult GuargarVideo()
        {
            Context.Medias
                .Add(new Infraestructura.Entities.MediaEntity { Nombre = "a", Ruta = "b" });
            Context.SaveChanges();
            return Ok();
        }




    }

    public class VideosResult
    {
        public string indice { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string tags { get; set; }
        public string autor { get; set; }
        //public string duracion { get; set; }

        public VideosResult(string indice, string nombre, string descripcion, string tags, string autor)
        {
            this.indice = indice;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.tags = tags;
            this.autor = autor;
        }
    }

    public class PaginadoResponse
    {
        public int offset { get; set; }
        public int size { get; set; }

        public List<VideosResult> page {get; set; }
    
        public PaginadoResponse(int offset, int size, List<VideosResult> paginado)
        {
            this.offset = offset;
            this.size = size;
            this.page = paginado;
        }
    }

}
 
 