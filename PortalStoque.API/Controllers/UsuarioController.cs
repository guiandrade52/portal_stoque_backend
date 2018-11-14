using PortalStoque.API.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        static readonly IUserRepositorio _repositorio = new UserRepositorio();

        [HttpPost]
        [Route("GetUser")]
        public HttpResponseMessage GetUser(Login login)
        {
            if (login == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuário e senha deve ser informado");
           return Request.CreateResponse(HttpStatusCode.OK, _repositorio.GetUsuario(login.UserName, login.Password));
        }        
    }
}
