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
        public HttpResponseMessage GetAll(string search)
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = QueryContrato.GetFilter(user, search);

            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetAll(filter));
        }

        public HttpResponseMessage GetEndereco(int contrato)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetDetails(contrato));
        }
    }
}
