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
using Streaming.Infraestructura.Entities;
using Streaming.Infraestructura.Repositories.contracts;

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
                string path = Path.GetFullPath(Repo.getMediaById(fileId).Ruta);
                return Repo.GetFile(path,this);
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
                List<VideosResult> resMap = resultado.Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre)) as List<VideosResult>;
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
                .Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre));
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

        public VideosResult(string indice, string nombre)
        {
            this.indice = indice;
            this.nombre = nombre;
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
 
 