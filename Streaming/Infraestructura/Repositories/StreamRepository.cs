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
            return GetMedias()
                .Select(pair => pair.Nombre) //esto es el map, viene por extension de LINQ; solo busca por nombre, deberia buscar por mas cosas. (categoria, popularidad, etc)
                .Where(pair => pair.Contains(sugerencia)).Take(10) // el condicional es mas grande, toma las primeras 10 sugerencias
                .ToListAsync();
        }

        public Task<List<MediaEntity>> SearchVideos(string busqueda)
        {
            return GetMedias()
                            .Where(pair => pair.Nombre.Contains(busqueda)) // solo busca por nombre, deberia buscar por mas cosas. (categoria, popularidad, etc)
                            .ToListAsync();
        }

        public int GetTotalVideos()
        {
            return GetMedias().Count();
        }

        public FileStreamResult GetFile(string path, ControllerBase controller)
        {
            var fileStream = System.IO.File.Open(path, FileMode.Open);
            return controller.File(fileStream, "application/octet-stream");
        }

        private DbSet<MediaEntity> GetMedias()
        {
            return ((MediaContext)_context).Medias;
        }
    }    
}
