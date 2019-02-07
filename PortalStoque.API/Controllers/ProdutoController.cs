using PortalStoque.API.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ProdutoController : ApiController
    {
        static readonly IProdutoRepositorio _produtoRepositorio = new ProdutoRepositorio();
        public HttpResponseMessage GetAll(int contrato, int? codGrupo)
        {

            return Request.CreateResponse(HttpStatusCode.OK, _produtoRepositorio.GetAll(QueryProduto.GetFilter(contrato, codGrupo)));
        }
    }
}
