using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class MediaEntity : Entity
    {
        public string monedaOrigen { get; set; }
        public string monedaDestino { get; set; }
        public float porcentaje { get; set; }

    }
}


