using PortalStoque.API.Models.Parceiros;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ParceiroAtController : ApiController
    {
        static readonly IParceiroRepositorio _parceiroRepositorio = new ParceiroRepositorio();
        public HttpResponseMessage GetAll(string search)
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = QueryParceiroAt.GetFilter(user, search);

            return Request.CreateResponse(HttpStatusCode.OK, _parceiroRepositorio.GetAll(filter));
        }
    }
}
