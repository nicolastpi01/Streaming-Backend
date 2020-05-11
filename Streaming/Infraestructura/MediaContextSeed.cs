
using Streaming.Infraestructura.Entities;
using System.Collections.Generic;
using System.Linq;

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
                    
                    TagEntity sports = new TagEntity { Nombre = "sports", Media = { } };
                    TagEntity news = new TagEntity { Nombre = "news", Media = { } };
                    TagEntity music = new TagEntity { Nombre = "music", Media = { } };
                    TagEntity movies = new TagEntity { Nombre = "movies", Media = { } };

                    /*
                    context.Tags.Add(new TagEntity { Nombre = sports.Nombre, Media = sports.Media });
                    context.Tags.Add(new TagEntity { Nombre = news.Nombre, Media = news.Media });
                    context.Tags.Add(new TagEntity { Nombre = music.Nombre, Media = music.Media });
                    context.Tags.Add(new TagEntity { Nombre = movies.Nombre, Media = movies.Media });
                    */


                    List<TagEntity> lista = new List<TagEntity>();
                    lista.Add(sports);
                    lista.Add(movies);
                    //lista.Add(news);
                    //lista.Add(music);

                    /*
                    List<TagEntity> lista2 = new List<TagEntity>();
                    lista.Add(news);
                    lista.Add(music);
                    */

                    context.Medias.Add(new MediaEntity { Nombre = "1280", Ruta = "Movies/1280.mp4", Descripcion = "Una descripcion", Tags= lista, Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "sampleVideo1", Ruta = "Movies/sampleVideo_1280x720_10mb.mp4", Descripcion = "Una descripcion", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "sampleVideo2", Ruta = "Movies/sampleVideo_1280x720_5mb.mp4", Descripcion = "Una descripcion", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "small", Ruta = "Movies/small.mp4", Descripcion = "Una descripcion", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "dolbycanyon", Ruta = "Movies/dolbycanyon.mp4", Descripcion = "Una descripcion", Autor = "NicolasTskTsk" });
                    context.Medias.Add(new MediaEntity { Nombre = "star_trails", Ruta = "Movies/star_trails.mp4", Descripcion = "Una descripcion", Autor = "NicolasTskTsk" });

                    
                }
                context.SaveChanges();
            }
        }
    }

}

