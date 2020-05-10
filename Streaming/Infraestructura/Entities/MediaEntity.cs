using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class MediaEntity : Entity
    {
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public string Tags { get; set; } // Example -> sports, movies, music, others, etc 
        public string Autor { get; set; }

        // Fecha de subida (cuando se crea el video)

        // duracion (se calcular cuando se crea el video / sube)
    }
}


