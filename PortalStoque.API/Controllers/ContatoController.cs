using PortalStoque.API.Models.Contatos;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ContatoController : ApiController
    {
        static readonly IContatoRepositorio _contratoRepositorio = new ContatoRepositorio();
        public HttpResponseMessage GetAll(string search)
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = QueryContato.GetFilter(user, search);

            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetAll(filter));
        }

        public HttpResponseMessage GetComContrato(int contrato, int codParc)
        {
            string filter = QueryContato.GetFilterSemSerie(codParc, contrato);
            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetComContrato(filter));
        }

        public HttpResponseMessage GetComSerie(string serie)
        {
            string filter = QueryContato.getFilterComSerie(serie);
            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetComSerie(filter));
        }


    }
}
