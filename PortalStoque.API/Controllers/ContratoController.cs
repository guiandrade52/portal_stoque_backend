using PortalStoque.API.Models.Contratos;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ContratoController : ApiController
    {
        static readonly IContratoRepositorio _contratoRepositorio = new ContratoRepositorio();
        public HttpResponseMessage GetAll()
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = Query.GetFilter(user);          

            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetAll(filter));
        }
    }
}
