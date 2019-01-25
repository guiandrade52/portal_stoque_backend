using PortalStoque.API.Models.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{   
    [Authorize]
    public class ServicoController : ApiController
    {
        static readonly IServicoRepositorio _servicoRepositorio = new ServicoRepositorio();
        public HttpResponseMessage GetAll()
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = QueryServico.GetFilter(user);

            return Request.CreateResponse(HttpStatusCode.OK, _servicoRepositorio.GetAll(filter));
        }
    }
}
