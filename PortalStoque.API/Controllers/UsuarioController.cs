using PortalStoque.API.Models.Usuarios;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("GetUser")]
        public HttpResponseMessage GetUsuario()
        {
            var u = new services.UsuarioCorrent();
            return Request.CreateResponse(HttpStatusCode.OK, u.GetPermisoes());
        }
    }
}
