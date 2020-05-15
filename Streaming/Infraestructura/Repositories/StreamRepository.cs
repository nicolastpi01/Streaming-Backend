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
                    //.Select(media => media.Ruta)
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

            result = GetMedias() /* revisar que sepa separar por comas los tags en las sugerencias oo crear otra tabla para los tags */
                           .Where(pair => pair.Nombre.Contains(lower))
                           .Select(pair => pair.Nombre);
            if (result.Count() > 0) return result.Take(1).ToListAsync();

            result = GetMedias() /* revisar este ultimo, se puede hacer algo mejor */
                   .Where(pair => pair.Descripcion.ToLower().Contains(lower))
                   .Select(pair => pair.Descripcion);
            return result.Take(1).ToListAsync();

            /*
             * return 
                    .Select(pair => pair.Nombre)
                    .Where(pair => pair.Contains(sugerencia)).Take(10).ToListAsync();*/
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

        public FileStreamResult GetFile(string path, ControllerBase controller)
        {
            string pathBase = Path.GetFullPath("Streaming");
            var fileStream = System.IO.File.Open(pathBase + path, FileMode.Open);
            return controller.File(fileStream, "application/octet-stream");
        }

        private DbSet<MediaEntity> GetMedias()
        {
            return ((MediaContext)_context).Medias;
        }
    }    
}
