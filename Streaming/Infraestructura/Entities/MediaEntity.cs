using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class MediaEntity : Entity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int MediaId { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        //public string Tags { get; set; } // Example -> sports, movies, music, others, etc

        public List<TagEntity> Tags { get; set; }
        public string Autor { get; set; }

        public bool CoincideTag(string tag)
        {
            bool ret = false;
            Tags.ForEach(t =>
            ret = ret || t.Nombre == tag);

            return ret;
        }

        // Fecha de subida (cuando se crea el video)

        // duracion (se calcular cuando se crea el video / sube)
    }
}


