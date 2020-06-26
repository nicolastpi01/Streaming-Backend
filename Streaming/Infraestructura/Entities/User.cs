using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class User : Entity
    {
        public string Nombre;
        internal readonly object Media;
        private readonly List<MediaEntity> _medias;
    }
}
