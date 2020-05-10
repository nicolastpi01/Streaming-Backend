using Microsoft.EntityFrameworkCore;
using Streaming.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura
{
    public class MediaContextSeed
    {
        
        private MediaContext _context;

        public void SeedAsync(MediaContext context)
        {
            this._context = context;
            using (context)
            {
                //ESTE SEED LO QUE HACE ES QUE SI NO HAY DATOS DE CONFIGURACION , QUE SE IMPORTANTAN DESDE ARCHIVOS CSV, SE REALIZA
                //LA MIGRACIÓN AUTOMATICAMENTE

                //context.Database.Migrate();
                
                if (!context.Medias.Any())
                {
                    
                    context.Medias.Add(new MediaEntity { Nombre = "1280", Ruta = "Movies/1280.mp4", Descripcion = "Una descripcion", Tags="movies, music", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "sampleVideo1", Ruta = "Movies/sampleVideo_1280x720_10mb.mp4", Descripcion = "Una descripcion", Tags = "other", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "sampleVideo2", Ruta = "Movies/sampleVideo_1280x720_5mb.mp4", Descripcion = "Una descripcion", Tags = "other", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "small", Ruta = "Movies/small.mp4", Descripcion = "Una descripcion", Tags = "other", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "dolbycanyon", Ruta = "Movies/dolbycanyon.mp4", Descripcion = "Una descripcion", Tags = "other", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "star_trails", Ruta = "Movies/star_trails.mp4", Descripcion = "Una descripcion", Tags = "other", Autor = "NicolasTskTsk" });
                }
                context.SaveChanges();
            }
        }
    }

}

