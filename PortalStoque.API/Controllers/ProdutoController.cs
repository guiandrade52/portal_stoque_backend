using PortalStoque.API.Models.Produtos;
using PortalStoque.API.Models.Usuarios;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ProdutoController : ApiController
    {
        static readonly IProdutoRepositorio _produtoRepositorio = new ProdutoRepositorio();
        public HttpResponseMessage GetAll(int codGrupo, int? contrato)
        {
            var u = new services.UsuarioCorrent();
            Permisoes permisoes = u.GetPermisoes();
            string filter = QueryProduto.GetFilter(codGrupo, contrato, permisoes);
            return Request.CreateResponse(HttpStatusCode.OK, _produtoRepositorio.GetAll(filter));
        }
    }
}
