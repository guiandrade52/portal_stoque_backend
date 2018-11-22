using PortalStoque.API.Models.Ocorrencias;
using PortalStoque.API.Models.Usuarios;
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
        static readonly IOcorrenciaRepositorio _OcorRepositorio = new OcorrenciaRepositorio();
        static readonly IUsuarioRepositorio _UserRepositorio = new UsuarioRepositorio();

        public HttpResponseMessage GetAll([FromUri]Filter pFilter)
        {
            var u = new services.CacheUserController();
            string filter = Query.GetFilter(pFilter, u.GetUser());

            List<Ocorrencia> Tasks = _OcorRepositorio.GetAll(filter, pFilter.Pagina, pFilter.TamPag).ToList();
            int TotalOcor = _OcorRepositorio.GetTotalOcor(filter);

            return Request.CreateResponse(HttpStatusCode.OK, new { Tasks, TotalOcor });
        }
    }
}
