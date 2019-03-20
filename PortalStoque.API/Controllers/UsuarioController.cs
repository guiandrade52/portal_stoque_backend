using PortalStoque.API.Models.Parceiros;
using PortalStoque.API.Models.Usuarios;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        static readonly IParceiroRepositorio _parceiroRepositorio = new ParceiroRepositorio();
        [HttpGet]
        public HttpResponseMessage GetUsuario()
        {
            var u = new services.UsuarioCorrent();
            var p = u.GetPermisoes();
            if (!string.IsNullOrEmpty(p.Contratos))
            {
                var Contratos = _parceiroRepositorio.ParceirosPorContrato(p.Contratos);
                return Request.CreateResponse(HttpStatusCode.OK, new { Permissoes = p, Usuario = u.GetUsuario(), Contratos });
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { Permissoes = p, Usuario = u.GetUsuario() });
        }
    }
}
