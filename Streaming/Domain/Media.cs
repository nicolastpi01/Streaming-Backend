using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Domain
{
    public class Media
    {
        public Media(string nombre, string ruta)
        {
            Nombre = nombre;
            Ruta = ruta;
        }

        public Guid Id { get; set; }
        public string Nombre { get ; set ; }
        public string Ruta { get; set; }

    }
}
