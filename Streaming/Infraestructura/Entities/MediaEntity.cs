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
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        
        private readonly List<TagEntity> _tags;
        public IReadOnlyCollection<TagEntity> Tags => _tags;
        public string Autor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public double MeGusta { get; set; }
        public double NoMeGusta { get; set; }

        public double Vistas { get; set; }

        public bool CoincideTag(string tag)
        {
            bool ret = false;
            _tags.ForEach(t =>
            ret = ret || t.Nombre == tag);

            return ret;
        }


        protected MediaEntity()
        {
            _tags = new List<TagEntity>();
            
        }

        public MediaEntity(string Nombre,
                  string Ruta,
                  string Descripcion,
                  string Autor,
                  string Imagen,
                  DateTime FechaCreacion,
                  double MeGusta,
                  double NoMeGusta,
                  double Vistas) : this()
        {
            this.Nombre = Nombre;
            this.Ruta = Ruta;
            this.Descripcion = Descripcion;
            this.Autor = Autor;
            this.Imagen = Imagen;
            this.FechaCreacion = FechaCreacion;
            this.MeGusta = MeGusta;
            this.NoMeGusta = NoMeGusta;
            this.Vistas = Vistas;
        }

        

        public void AddTag(int IdMedia, MediaEntity Media, string Nombre)
        {
            this._tags.Add(new TagEntity(IdMedia,
                                              Media,
                                              Nombre));
        }

        public void addMG()
        {
            this.MeGusta += 1;
        }

    }
}




