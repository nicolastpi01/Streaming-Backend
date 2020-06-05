using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Streaming.Controllers.Model;
using Streaming.Infraestructura.Entities;
using Streaming.Infraestructura.Repositories.contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Repositories
{
    public class StreamRepository : BaseRepository<MediaEntity>, IStreamRepository
    {
        public StreamRepository(DbContext context) : base(context)
        {
        }

        public override async Task<List<MediaEntity>> GetAll()
        {
            return await GetMedias().Select(x => x).ToListAsync();
        }

        public MediaEntity getMediaById(string Id)
        {
            return GetMedias()
                    .Where(media => media.Id.ToString().Equals(Id))
                    .Single();
        }

        public Task<List<MediaEntity>> PaginarMedia(int indice, int offset)
        {
            return GetMedias()
                .Skip(indice)
                .Take(offset)
                .ToListAsync();
        }

        public Task<List<string>> GetSugerencias(string sugerencia)
        {
            IQueryable<string> result;
            string lower = "";
            if (sugerencia != null) lower = sugerencia.ToLower();
            result = GetMedias()
                            .Where(pair => pair.Nombre.ToLower().Contains(lower))
                            .Select(pair => pair.Nombre);
            if (result.Count() > 0) return result.Take(10).ToListAsync();

            result = GetMedias()
                           .Where(pair => pair.Autor.ToLower().Contains(lower))
                           .Select(pair => pair.Autor);
            if (result.Count() > 0) return result.Take(1).ToListAsync();

            result = GetTags() // Retorna un tag que coincide con la busqueda, tocar la busqueda al apretar el boton para que tenga en cuenta los tags 
                           .Where(pair => pair.Nombre.Contains(lower))
                           .Select(pair => pair.Nombre);

            if (result.Count() > 0) return result.Take(1).ToListAsync();


            result = GetMedias() /* revisar este ultimo, se puede hacer algo mejor */
                   .Where(pair => pair.Descripcion.ToLower().Contains(lower))
                   .Select(pair => pair.Nombre);
            return result.Take(1).ToListAsync();

        }

        public Task<List<MediaEntity>> SearchVideos(string busqueda)
        {
            return GetMedias()
                .Where(pair => pair.Nombre.Contains(busqueda) || pair.Autor.Contains(busqueda) || pair.Descripcion.Contains(busqueda) )//|| pair.Tags.Contains(busqueda)) // solo busca por nombre, deberia buscar por mas cosas. (categoria, popularidad, etc)
                .ToListAsync();
        }

        public int GetTotalVideos()
        {
            return GetMedias().Count();
        }

        //public FileStreamResult GetFileById(string path, ControllerBase controller)
        public FileStreamResult GetFileById(string fileId, ControllerBase controller)
        {
            var ruta = getMediaById(fileId).Ruta;
            string path = Path.GetFullPath(ruta);
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            return controller.File(fileStream, "application/octet-stream");
        }
        public FileStreamResult GetImagenById(string fileId, ControllerBase controller)
        {
            var ruta = getMediaById(fileId).Imagen;
            string path = Path.GetFullPath(ruta);
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            return controller.File(fileStream, "application/octet-stream");
        }

        public void SaveMedia(PublishMedia mediapublicada)
        {
            var procesos = new List<Task> {
                SaveVideo(mediapublicada.video, mediapublicada.nombre),
                SaveVideo(mediapublicada.imagen,mediapublicada.nombre),
            }.ToArray();
            Task.WhenAll(procesos).ContinueWith(async (state) =>
            {
                if (procesos.Any(t => t.IsFaulted)) return;
                var media = new MediaEntity(mediapublicada.nombre, CrearRuta(mediapublicada.nombre,mediapublicada.video.FileName), "Una descripcion", "NiocolasTsk", CrearRuta(mediapublicada.nombre, mediapublicada.imagen.FileName));
                GetMedias().Add(media);
                _context.SaveChanges();
            }).Wait();
        }

        public async Task<string> SaveVideo(IFormFile archivo, string name)
        {   
            //Buscar los tipos: https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
            var tipo = archivo.ContentType.Contains("video") ? "video" :
                archivo.ContentType.Contains("image") ? "imagen" :
                "incompatible";

            if (archivo.Length > 0 && FormatosAceptables(tipo, archivo.FileName))
            {
                var ruta = CrearRuta(name, archivo.FileName);
                var filePath = Path.GetFullPath(ruta);  //Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await archivo.CopyToAsync(stream);
                }

                return ruta;
            }
            return "";
            //explorar el uso de videoController.PhysicalFile()
            //videoController.Created
        }

        private DbSet<MediaEntity> GetMedias()
        {
            return ((MediaContext)_context).Medias;
        }

        private DbSet<TagEntity> GetTags()
        {
            return ((MediaContext)_context).Tags;
        }

        private bool FormatosAceptables(string tipo, string nombreArchivo)
        {
            switch (tipo)
            {
                case "video": return nombreArchivo.Contains("mp4");
                case "imagen": return nombreArchivo.Contains("png");
                default: return false;
            }
        }

        //esto seria en la ubicacion de contenidos del usuario
        // ".mp4" o ".png"
        private string CrearRuta(string name, string archivo) => "/StreamingMovies/" + name + "." + archivo.Split(".").Last(); 
        
    }    
}
