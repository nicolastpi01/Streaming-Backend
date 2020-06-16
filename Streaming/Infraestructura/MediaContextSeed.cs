
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

                    
                    MediaEntity m1 = new MediaEntity("1280", "/StreamingMovies/1280.mp4", "Una descripcion", "Nikolai Luzhin", "/StreamingMovies/EasternPromises.png");
                    MediaEntity m2 = new MediaEntity("sampleVideo1", "/StreamingMovies/sampleVideo_1280x720_10mb.mp4", "Una descripcion", "Tom Hagen", "/StreamingMovies/TomHagen.png");
                    MediaEntity m3 = new MediaEntity("sampleVideo2", "/StreamingMovies/sampleVideo_1280x720_5mb.mp4", "Una descripcion", "Vincent Corleone", "/StreamingMovies/VincentCorleone.png");
                    MediaEntity m4 = new MediaEntity("small", "/StreamingMovies/small.mp4", "Una descripcion", "Proposition Joe", "/StreamingMovies/PropositionJoe.png");
                    MediaEntity m5 = new MediaEntity("dolbycanyon", "/StreamingMovies/dolbycanyon.mp4", "Una descripcion", "TonySoprano", "/StreamingMovies/TonySoprano.png");
                    MediaEntity m6 = new MediaEntity("star_trails", "/StreamingMovies/star_trails.mp4", "Una descripcion", "SonnyCorleone", "/StreamingMovies/SonnyCorleone.png");

                    // NUEVOS
                    
                    MediaEntity m7 = new MediaEntity("Panasonic_HDC_TM_700_P_50i", "/StreamingMovies/Panasonic_HDC_TM_700_P_50i.mp4", "Una descripcion", "Spawn666");
                    MediaEntity m8 = new MediaEntity("page18-movie-4", "/StreamingMovies/page18-movie-4.mp4", "Una descripcion", "Spawn666"); 
                    MediaEntity m9 = new MediaEntity("grb_2", "/StreamingMovies/grb_2.mp4", "Una descripcion", "Spawn666");
                    MediaEntity m10 = new MediaEntity("metaxas-keller-Bell", "/StreamingMovies/metaxas-keller-Bell.mp4", "Una descripcion", "Spawn666");
                    MediaEntity m11 = new MediaEntity("TRA3106", "/StreamingMovies/TRA3106.mp4", "Una descripcion", "Stringer Bell");
                    MediaEntity m12 = new MediaEntity("lion-sample", "/StreamingMovies/lion-sample.mp4", "Una descripcion", "CorleoneVito");
                    MediaEntity m13 = new MediaEntity("jellyfish-25-mbps-hd-hevc", "/StreamingMovies/jellyfish-25-mbps-hd-hevc.mp4", "Una descripcion", "Spawn666");
                    MediaEntity m14 = new MediaEntity("P6090053", "/StreamingMovies/P6090053.mp4", "Una descripcion", "Don Eraldo");

                    MediaEntity m15 = new MediaEntity("SampleVideos3", "/StreamingMovies/SampleVideos3.mp4", "Una descripcion", "Don Eraldo");
                    MediaEntity m16 = new MediaEntity("SampleVideos4", "/StreamingMovies/SampleVideos4.mp4", "Una descripcion", "Don Eraldo");
                    MediaEntity m17 = new MediaEntity("SampleVideos5", "/StreamingMovies/SampleVideos5.mp4", "Una descripcion", "Don Eraldo");
                    MediaEntity m18 = new MediaEntity("SampleVideos6", "/StreamingMovies/SampleVideos6.mp4", "Una descripcion", "Don Eraldo");
                    


                    


                    m1.AddTag(m1.Id, m1, "sports");
                    m1.AddTag(m1.Id, m1, "movies");
                    m1.AddTag(m1.Id, m1, "music");

                    m2.AddTag(m1.Id, m2, "news");


                    context.Medias.Add(m1);
                    context.Medias.Add(m2);
                    context.Medias.Add(m3);
                    context.Medias.Add(m4);

                    context.Medias.Add(m5);
                    context.Medias.Add(m6);
                    
                    context.Medias.Add(m7);
                    context.Medias.Add(m8);
                    context.Medias.Add(m9);
                    context.Medias.Add(m10);
                    context.Medias.Add(m11);
                    context.Medias.Add(m12);
                    context.Medias.Add(m13);
                    context.Medias.Add(m14);
                    context.Medias.Add(m15);
                    context.Medias.Add(m16);
                    context.Medias.Add(m17);
                    context.Medias.Add(m18);
                    
                }
                context.SaveChanges();
            }
        }
    }

}

