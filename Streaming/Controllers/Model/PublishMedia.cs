using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Controllers.Model
{
    public class PublishMedia
    {
        public IFormFile video { get; set; }
        public IFormFile imagen { get; set; }
        public string nombre { get; set; }
    }
}
