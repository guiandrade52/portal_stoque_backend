using PortalStoque.API.Models.GrupoProdutos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class GrupoProdutoController : ApiController
    {
        static readonly IGrupoProduto _grupoProdutoRepositorio = new GrupoProdutoRepositorio();
        public HttpResponseMessage GetAll(int contrato)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _grupoProdutoRepositorio.GetAll(contrato));
        }
    }
}
