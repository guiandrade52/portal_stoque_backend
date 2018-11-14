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

        public HttpResponseMessage GetAllOcorrencia([FromUri]FilterOcorModel filter)
        {
            var pWhere = "";

            List<Ocorrencia> listaOcorrencia = ocorrenciaRepositorio.GetAll(filter.Pagina, filter.TamPag, pWhere).ToList();
            var result = new { Tasks = listaOcorrencia, TotalOcor = ocorrenciaRepositorio.TotalOcorrencia(pWhere) };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
