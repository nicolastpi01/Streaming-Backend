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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Streaming.Controllers.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        //[Authorize]
        public async Task<PaginadoResponse> GetVideos(int page)
        {
            if (page > 0) page--;
            else return null;

            var indice = page * offset;

            try
            {
                List<MediaEntity> resultado = await Repo.PaginarMedia(indice, offset);
                var resMap = resultado.Select(pair => new VideosResult(pair.Id.ToString(), pair.Nombre, pair.Descripcion, pair.Autor, pair.FechaCreacion, pair.MeGusta, pair.NoMeGusta, pair.Vistas)).ToList() as List<VideosResult>;
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

        [HttpPost]
        [Route("saveFile")]
        [AllowAnonymous]
        public async Task<ActionResult> saveFile([FromForm] PublishMedia mediapublicada)
        {
            Repo.SaveMedia(mediapublicada);
            return Ok(new { Status = "grabado"});
        }

        [HttpPost]
        [Route("like")]
        //[ReadableBodyStream]
        [AllowAnonymous]
        public async Task<ActionResult> AddLike(LikeRequest request)
        {
            
                Repo.AddLike(request.mediaId);
                // body = "param=somevalue&param2=someothervalue"
            
            
            //var media = Repo.getMediaById(mediaId);
            //media.addMG();
            //Repo.Update(media);

            return Ok(new { Status = "grabado" });
        }

        

        [HttpGet]
        [Route("sugerencias")]
        public Task<List<string>> GetSugerencias(string sugerencia) // las sugerencias para una posible busqueda
        {
            return Repo.GetSugerencias(sugerencia);
        }
        
    }

    public class LikeRequest
    {
        public string mediaId { get; set; }

            public LikeRequest()
        {

        }

        public LikeRequest(string mediaId)
        {
            this.mediaId = mediaId;
        }
    }

    public class VideosResult
    {
        public string indice { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        //public List<TagEntity> tags { get; set; }
        public string autor { get; set; }
        public DateTime fechaCreacion { get; }
        public double meGusta { get; }
        public double noMeGusta { get; }
        public double vistas { get; }


        public VideosResult(string indice, string nombre, string descripcion, string autor)
        {
            this.indice = indice;
            this.nombre = nombre;
            this.descripcion = descripcion;
            //this.tags = tags;
            this.autor = autor;
        }

        public VideosResult(string indice, string nombre, string descripcion, string autor, DateTime fechaCreacion, double meGusta, double noMeGusta, double vistas) : this(indice, nombre, descripcion, autor)
        {
            this.indice = indice;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.autor = autor;
            this.fechaCreacion = fechaCreacion; // que retorne el tiempo de forma mas amigable
            this.meGusta = meGusta;
            this.noMeGusta = noMeGusta;
            this.vistas = vistas;
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
 
 