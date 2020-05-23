
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
                    MediaEntity m1 = new MediaEntity("1280", "/StreamingMovies/1280.mp4", "Una descripcion", "NiocolasTsk", "/StreamingMovies/montaña.png");
                    MediaEntity m2 = new MediaEntity("sampleVideo1", "/StreamingMovies/sampleVideo_1280x720_10mb.mp4", "Una descripcion", "Mamushka01", "/StreamingMovies/bigbunny1.png");
                    MediaEntity m3 = new MediaEntity("sampleVideo2", "/StreamingMovies/sampleVideo_1280x720_5mb.mp4", "Una descripcion", "PireSusuke", "/StreamingMovies/bigbunny2.png");
                    MediaEntity m4 = new MediaEntity("small", "/StreamingMovies/small.mp4", "Una descripcion", "OtooOCTAVIUS", "/StreamingMovies/experimento.png");
                    //MediaEntity m5 = new MediaEntity("dolbycanyon", "/StreamingMovies/dolbycanyon.mp4", "Una descripcion", "DrossRotzank" , "/StreamingMovies/bigbunny.png");
                    //MediaEntity m6 = new MediaEntity("star_trails", "/StreamingMovies/star_trails.mp4", "Una descripcion", "KingsAndGenerals", "/StreamingMovies/bigbunny.png");


                    m1.AddTag(m1.Id, m1, "sports");
                    m1.AddTag(m1.Id, m1, "movies");
                    m1.AddTag(m1.Id, m1, "music");

                    m2.AddTag(m1.Id, m2, "news");


                    context.Medias.Add(m1);
                    context.Medias.Add(m2);
                    context.Medias.Add(m3);
                    context.Medias.Add(m4);
                    //context.Medias.Add(m5);
                    //context.Medias.Add(m6);
                }
                context.SaveChanges();
            }
        }
    }

}

