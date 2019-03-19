using PortalStoque.API.Models.GrupoProdutos;
using PortalStoque.API.Models.Usuarios;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class GrupoProdutoController : ApiController
    {
        static readonly IGrupoProdutoRepositorio _grupoProdutoRepositorio = new GrupoProdutoRepositorio();
        public HttpResponseMessage GetAll()
        {
            var u = new services.UsuarioCorrent();
            Permisoes permisoes = u.GetPermisoes();
            string filter = QueryGrupoProduto.GetFilter(permisoes);
            return Request.CreateResponse(HttpStatusCode.OK, _grupoProdutoRepositorio.GetAll(filter));
        }

        public HttpResponseMessage GetAll(int contrato)
        {
            string filter = QueryGrupoProduto.GetFilterContrato(contrato);
            return Request.CreateResponse(HttpStatusCode.OK, _grupoProdutoRepositorio.GetAll(filter));
        }
    }
}
