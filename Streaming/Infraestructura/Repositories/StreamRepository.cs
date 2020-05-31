using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public Task SaveVideo(IFormFile archivo, string name, Controllers.VideoController videoController)
        {
            var ruta = "/StreamingMovies/"; //esto seria en la ubicacion de contenidos del usuario
            
            if (archivo.Length > 0)
            {
                var filePath = Path.GetFullPath(ruta + name);  //Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    return archivo.CopyToAsync(stream);
                }
            }
            else
                return null;
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

    }    
}
