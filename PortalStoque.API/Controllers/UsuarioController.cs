using PortalStoque.API.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    
    public class UsuarioController : ApiController
    {
        static readonly IUserRepositorio _repositorio = new UserRepositorio();

        [Route("api/GetUser")]
        [HttpGet]
        public Usuario GetUser(string pLogin, string pSenha)
        {
           return _repositorio.GetUsuario(pLogin, pSenha);
        }

        [Authorize(Roles = "Gestor")]
        [Route("SoAdministrador")]
        [HttpGet]
        public string SoAdministrador()
        {
            return "Usuário Administrador.";
        }
    }
}
