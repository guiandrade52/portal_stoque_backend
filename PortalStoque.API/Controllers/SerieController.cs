using PortalStoque.API.Models.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class SerieController : ApiController
    {
        static readonly ISerieRepositorio _serieRepositorio = new SerieRepositorio();
        public HttpResponseMessage GetAll(string search)
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            return Request.CreateResponse(HttpStatusCode.OK, _serieRepositorio.GetAll(QuerySerie.GetFilter(user, search)));
        }

        public HttpResponseMessage GetAll(int contrato, int codProd, int codGrupo)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _serieRepositorio.GetAll(QuerySerie.GetFilterSerProd(contrato, codProd, codGrupo)));
        }

        public HttpResponseMessage GetDetails(string serie)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _serieRepositorio.GetSerieDetails(serie));
        }

    }
}
