using PortalStoque.API.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Caching;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        static readonly IUserRepositorio _repositorio = new UserRepositorio();

        [HttpPost]
        [Route("GetUser")]
        public HttpResponseMessage GetUser()
        {
            string userId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;            
            return Request.CreateResponse(HttpStatusCode.OK, _repositorio.GetCurrentUser(userId));
        }        
    }
}
