using PortalStoque.API.Models.Ocorrencia;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        static readonly IOcorrenciaRepositorio ocorrenciaRepositorio = new OcorrenciaRepositorio();

        public HttpResponseMessage GetAllOcorrencia(int pPagina, int pTamPag )
        {
            var pWhere = "";

            List<Ocorrencia> listaOcorrencia = ocorrenciaRepositorio.GetAll(pPagina, pTamPag, pWhere).ToList();
            var result = new { Tasks = listaOcorrencia, TotalOcor = ocorrenciaRepositorio.TotalOcorrencia(pWhere) };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetFilterDefault([FromUri]FilterOcorModel defaultFilter)
        {

            List<Ocorrencia> listaOcorrencia = ocorrenciaRepositorio.GetAll(1,2, null).ToList();
            var result = new { Tasks = listaOcorrencia, TotalOcor = ocorrenciaRepositorio.TotalOcorrencia(null) };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
