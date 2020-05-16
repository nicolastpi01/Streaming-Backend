using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Streaming.Infraestructura;
using Streaming.Infraestructura.Entities;
using Streaming.Infraestructura.Repositories.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public MediaContext Context { get; private set; }

        public AuthController(DbContext context)
        {
            Context = context as MediaContext;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult RegisterUser([FromBody] string body) //string NewAlias, string newPass, string newMail)
        {
            var usuario =  new JsonObject(body).Object as UserEntity;
            Context.Users.Add(new UserEntity { Alias = usuario.Alias, Pass = usuario.Pass, Mail = usuario.Mail });
            return Ok("UsuarioCreado");
        }


    }
}
