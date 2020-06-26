
using Streaming.Infraestructura.Entities;
using System;
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

                    
                    MediaEntity m1 = new MediaEntity("Fish25061", "/StreamingMovies/Fish25061.mp4", "Una descripcion", "Nikolai Luzhin", "/StreamingMovies/images/soprano1.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m2 = new MediaEntity("Footage13496", "/StreamingMovies/Footage13496.mp4", "Una descripcion", "Tom Hagen", "/StreamingMovies/images/soprano2.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m3 = new MediaEntity("France34404", "/StreamingMovies/France34404.mp4", "Una descripcion", "Vincent Corleone", "/StreamingMovies/images/soprano3.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m4 = new MediaEntity("Gaming37585", "/StreamingMovies/Gaming37585.mp4", "Una descripcion", "Proposition Joe", "/StreamingMovies/images/soprano4.png", DateTime.Now, 200, 5, 300000);


                    MediaEntity m5 = new MediaEntity("Abstract4027", "/StreamingMovies/Abstract4027.mp4", "Una descripcion", "TonySoprano", "/StreamingMovies/images/default1.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m6 = new MediaEntity("Abstract14696", "/StreamingMovies/Abstract14696.mp4", "Una descripcion", "SonnyCorleone", "/StreamingMovies/images/default2.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m7 = new MediaEntity("Abstract14698", "/StreamingMovies/Abstract14698.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default3.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m8 = new MediaEntity("Abstract20072", "/StreamingMovies/Abstract20072.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default4.png", DateTime.Now, 200, 5, 300000); 
                    MediaEntity m9 = new MediaEntity("Abstract21161", "/StreamingMovies/Abstract21161.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default5.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m10 = new MediaEntity("Alien6080", "/StreamingMovies/Alien6080.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default6.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m11 = new MediaEntity("Atoms5205", "/StreamingMovies/Atoms5205.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default7.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m12 = new MediaEntity("Background6266", "/StreamingMovies/Background6266.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default8.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m13 = new MediaEntity("Background12620", "/StreamingMovies/Background12620.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default9.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m14 = new MediaEntity("Backgrounds13121", "/StreamingMovies/Backgrounds13121.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default10.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m15 = new MediaEntity("Ballerina25328", "/StreamingMovies/Ballerina25328.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default11.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m16 = new MediaEntity("Bicycle2310", "/StreamingMovies/Bicycle2310.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default12.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m17 = new MediaEntity("Bokeh5243", "/StreamingMovies/Bokeh5243.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default13.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m18 = new MediaEntity("Circles5211", "/StreamingMovies/Circles5211.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default14.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m19 = new MediaEntity("Colours14506", "/StreamingMovies/Colours14506.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default15.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m20 = new MediaEntity("Colours26496", "/StreamingMovies/Colours26496.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default16.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m21 = new MediaEntity("Cubes5067", "/StreamingMovies/Cubes5067.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default17.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m22 = new MediaEntity("District4462", "/StreamingMovies/District4462.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default18.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m23 = new MediaEntity("Electronics9178", "/StreamingMovies/Electronics9178.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default19.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m24 = new MediaEntity("Fantasy14491", "/StreamingMovies/Fantasy14491.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default20.png", DateTime.Now, 200, 5, 300000);



                    MediaEntity m25 = new MediaEntity("Germany1203", "/StreamingMovies/Germany1203.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano5.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m26 = new MediaEntity("Graphs2320", "/StreamingMovies/Graphs2320.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano6.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m27 = new MediaEntity("Idea2335", "/StreamingMovies/Idea2335.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano7.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m28 = new MediaEntity("InfiniteZoom25034", "/StreamingMovies/InfiniteZoom25034.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano8.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m29 = new MediaEntity("InfiniteZoom25037", "/StreamingMovies/InfiniteZoom25037.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano9.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m30 = new MediaEntity("InfiniteZoom25039", "/StreamingMovies/InfiniteZoom25039.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano10.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m31 = new MediaEntity("InfiniteZoom26138", "/StreamingMovies/InfiniteZoom26138.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano11.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m32 = new MediaEntity("InfiniteZoom26142", "/StreamingMovies/InfiniteZoom26142.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano12.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m33 = new MediaEntity("Ink18253", "/StreamingMovies/Ink18253.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano13.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m34 = new MediaEntity("Ink21536", "/StreamingMovies/Ink21536.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano14.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m35 = new MediaEntity("Kaleidoscope12499", "/StreamingMovies/Kaleidoscope12499.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/soprano15.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m36 = new MediaEntity("Kaleidoscope20524", "/StreamingMovies/Kaleidoscope20524.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire1.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m37 = new MediaEntity("KaleidoscopeArt17628", "/StreamingMovies/KaleidoscopeArt17628.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire2.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m38 = new MediaEntity("Lines4416", "/StreamingMovies/Lines4416.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire3.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m39 = new MediaEntity("Love32590", "/StreamingMovies/Love32590.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire4.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m40 = new MediaEntity("Macro33842", "/StreamingMovies/Macro33842.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire5.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m41 = new MediaEntity("Macro34057", "/StreamingMovies/Macro34057.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire6.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m42 = new MediaEntity("Mammoth17660", "/StreamingMovies/Mammoth17660.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire7.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m43 = new MediaEntity("Music35889", "/StreamingMovies/Music35889.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire8.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m44 = new MediaEntity("Neon21368", "/StreamingMovies/Neon21368.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire9.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m45 = new MediaEntity("Octagon5192", "/StreamingMovies/Octagon5192.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire10.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m46 = new MediaEntity("ScienceFiction6421", "/StreamingMovies/ScienceFiction6421.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/thewire11.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m47 = new MediaEntity("Squares4139", "/StreamingMovies/Squares4139.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default21.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m48 = new MediaEntity("Subscribe21657", "/StreamingMovies/Subscribe21657.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default22.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m49 = new MediaEntity("Sweet1378", "/StreamingMovies/Sweet1378.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default23.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m50 = new MediaEntity("Technology12903", "/StreamingMovies/Technology12903.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default24.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m51 = new MediaEntity("Tunnel12904", "/StreamingMovies/Tunnel12904.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default25.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m52 = new MediaEntity("VideoGame7249", "/StreamingMovies/VideoGame7249.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default26.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m53 = new MediaEntity("VirtualWorld15263", "/StreamingMovies/VirtualWorld15263.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default27.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m54 = new MediaEntity("Water33406", "/StreamingMovies/Water33406.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default28.png", DateTime.Now, 200, 5, 300000);
                    MediaEntity m55 = new MediaEntity("Yellow27803", "/StreamingMovies/Yellow27803.mp4", "Una descripcion", "Spawn666", "/StreamingMovies/images/default29.png", DateTime.Now, 200, 5, 300000);


                    m1.AddTag(m1.Id, m1, "sports");
                    m1.AddTag(m1.Id, m1, "movies");
                    m1.AddTag(m1.Id, m1, "music");
                    m2.AddTag(m1.Id, m2, "news");

                    ;

                    context.Medias.Add(m5);
                    context.Medias.Add(m6);
                    context.Medias.Add(m17);
                    context.Medias.Add(m18);
                    context.Medias.Add(m1);
                    context.Medias.Add(m2);
                    context.Medias.Add(m3);
                    context.Medias.Add(m4);
                    context.Medias.Add(m19);
                    context.Medias.Add(m20);
                    context.Medias.Add(m21);
                    context.Medias.Add(m29);
                    context.Medias.Add(m30);
                    context.Medias.Add(m31);
                    context.Medias.Add(m32);
                    context.Medias.Add(m33);
                    context.Medias.Add(m34);
                    context.Medias.Add(m35);
                    context.Medias.Add(m36);
                    context.Medias.Add(m37);
                    context.Medias.Add(m22);
                    context.Medias.Add(m23);
                    context.Medias.Add(m24);
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
                    context.Medias.Add(m24);
                    context.Medias.Add(m25);
                    context.Medias.Add(m26);
                    context.Medias.Add(m27);
                    context.Medias.Add(m28);
                    context.Medias.Add(m38);
                    context.Medias.Add(m39);
                    context.Medias.Add(m40);
                    context.Medias.Add(m41);
                    context.Medias.Add(m48);
                    context.Medias.Add(m49);
                    context.Medias.Add(m50);
                    context.Medias.Add(m51);
                    context.Medias.Add(m52);
                    context.Medias.Add(m53);
                    context.Medias.Add(m54);
                    context.Medias.Add(m55);
                    context.Medias.Add(m42);
                    context.Medias.Add(m43);
                    context.Medias.Add(m44);
                    context.Medias.Add(m45);
                    context.Medias.Add(m46);
                    context.Medias.Add(m47);
                    


                }
                context.SaveChanges();
            }
        }
    }

}

