using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class TagEntity : Entity
    {
        public int IdMedia { get; private set; }
        public MediaEntity Media { get; private set; }
        public string Nombre { get; set; }


        protected TagEntity()
        {

        }
        public TagEntity(int IdMedia,
                          MediaEntity Media,
                          string Nombre) : this()
        {
            this.IdMedia = IdMedia;
            this.Media = Media;
            this.Nombre = Nombre;
        }

    }

        
}



        

