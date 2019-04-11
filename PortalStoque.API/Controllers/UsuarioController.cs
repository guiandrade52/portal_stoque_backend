using PortalStoque.API.Models.Contratos;
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
        static readonly IContratoRepositorio _contratoRepositorio = new ContratoRepositorio();
        static readonly IUsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        [HttpGet]
        public HttpResponseMessage GetUsuario()
        {
            var u = new services.UsuarioCorrent();
            var p = u.GetPermisoes();
            if (!string.IsNullOrEmpty(p.Contratos))
            {
                var Contratos = _contratoRepositorio.ListaContratosPUsuario(p.IdUsuario);
                return Request.CreateResponse(HttpStatusCode.OK, new { Permissoes = p, Usuario = u.GetUsuario(), Contratos });
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { Permissoes = p, Usuario = u.GetUsuario() });
        }

        [HttpPost]
        public HttpResponseMessage UpdateData(Usuario usuario)
        {
            if (_usuarioRepositorio.UpdateData(usuario))
                return Request.CreateResponse(HttpStatusCode.OK);
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Não conseguimos atualizar os dados, tente novamente mais tarde, ou entre em contato com suporte." });
        }
    }
}
