using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Streaming.Infraestructura.Entities;
using Streaming.Infraestructura.Repositories.contracts;
using Microsoft.AspNetCore.Routing;


namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        public IStreamRepository Repo { get; private set; }

        private int offset;

        public VideoController(IStreamRepository repo)
        {
            Repo = repo;
            offset = 10;
        }

        [HttpGet]
        [Route("getFileById")]
        public ActionResult getFileById(string fileId)
        {
            try
            {
                //return Repo.GetFileById(Repo.getMediaById(fileId).Ruta, this);
                return Repo.GetFileById(fileId, this);
            }
            catch (InvalidOperationException)
            {
                return new EmptyResult();
            }
        }

        [HttpGet]
        [Route("getImagenById")]
        public ActionResult getImagenById(string fileId)
        {
            try
            {
                return Repo.GetImagenById(fileId, this);
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
            var indice = page * offset;

            try
            {
                List<MediaEntity> resultado = await Repo.PaginarMedia(indice, offset);
                var resMap = resultado.Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre, pair.Descripcion, pair.Autor)).ToList() as List<VideosResult>;
                return new PaginadoResponse(offset, Repo.GetTotalVideos(), resMap);
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
        public async Task<IEnumerable<VideosResult>> getSearchVideos(string busqueda)
        {
            return (await Repo.SearchVideos(busqueda))
                .Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre, pair.Descripcion, pair.Autor)); // pair.Tags,
        }



        
        [HttpGet]
        [Route("sugerencias")]
        public Task<List<string>> GetSugerencias(string sugerencia) // las sugerencias para una posible busqueda
        {
            return Repo.GetSugerencias(sugerencia);
        }
        
    }

    public class VideosResult
    {
        public string indice { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        //public List<TagEntity> tags { get; set; }
        public string autor { get; set; }
        //public string duracion { get; set; }

        public VideosResult(string indice, string nombre, string descripcion, string autor)
        {
            this.indice = indice;
            this.nombre = nombre;
            this.descripcion = descripcion;
            //this.tags = tags;
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
 
 