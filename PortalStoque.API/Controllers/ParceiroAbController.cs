using PortalStoque.API.Models.Parceiros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ParceiroAbController : ApiController
    {
        static readonly IParceiroRepositorio _parceiroRepositorio = new ParceiroRepositorio();
        public HttpResponseMessage GetAll(string search)
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = QueryParceiroAb.GetFilter(user, search);

            return Request.CreateResponse(HttpStatusCode.OK, _parceiroRepositorio.GetAll(filter));
        }
    }
}
