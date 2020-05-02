using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Entities
{
    public class UserEntity : Entity
    {
        public string Alias { get; set; }
        public string Mail { get; set; }
    }
}
