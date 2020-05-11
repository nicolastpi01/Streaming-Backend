using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class TagEntity : Entity
    {
        //public int MediaId { get; set; }
        public string Nombre { get; set; }
        public MediaEntity Media { get; set; }
    }
}
