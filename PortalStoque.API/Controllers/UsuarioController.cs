using PortalStoque.API.Models.Usuarios;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        static readonly IUsuarioRepositorio _repositorio = new UsuarioRepositorio();

        [HttpPost]
        [Route("GetUser")]
        public HttpResponseMessage GetUser()
        {
            var u = new services.UsuarioCorrent();
            return Request.CreateResponse(HttpStatusCode.OK, u.GetPermisoes());
        }        
    }
}
