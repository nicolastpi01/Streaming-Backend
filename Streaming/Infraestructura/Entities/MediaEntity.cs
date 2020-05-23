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

        public string Imagen { get; set; }
        //public string Tags { get; set; } // Example -> sports, movies, music, others, etc

        //public List<TagEntity> Tags { get; set; }

        private readonly List<TagEntity> _tags;
        public IReadOnlyCollection<TagEntity> Tags => _tags;

        public string Autor { get; set; }

        // Fecha de subida (cuando se crea el video)

        // duracion (se calcular cuando se crea el video / sube)

        protected MediaEntity()
        {
            _tags = new List<TagEntity>();
            
        }

        public MediaEntity(string Nombre,
                  string Ruta,
                  string Descripcion,
                  string Autor,
                  string Imagen) : this()
        {
            this.Nombre = Nombre;
            this.Ruta = Ruta;
            this.Descripcion = Descripcion;
            this.Autor = Autor;
            this.Imagen = Imagen;
        }

        public void AddTag(int IdMedia, MediaEntity Media, string Nombre)
        {
            this._tags.Add(new TagEntity( //IdMedia,
                                          //    Media,
                                              Nombre));
        }

    }
}


