using PortalStoque.API.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    public class UsuarioPortalController : ApiController
    {
        static readonly IUsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        public HttpResponseMessage GetAll()
        {
            var u = new services.UsuarioCorrent();
            var permisoes = u.GetPermisoes();
            string filter = QueryUsuario.GetFilter(permisoes);

            return Request.CreateResponse(HttpStatusCode.OK, _usuarioRepositorio.GetAll(filter));
        }
    }
}
