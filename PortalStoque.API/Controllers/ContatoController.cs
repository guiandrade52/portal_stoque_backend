using PortalStoque.API.Models.Contato;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    public class ContatoController : ApiController
    {
        static readonly IContatoRepositorio _contratoRepositorio = new ContatoRepositorio();
        public HttpResponseMessage GetAll()
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = Query.GetFilter(user);

            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetAll(filter));
        }
    }
}
