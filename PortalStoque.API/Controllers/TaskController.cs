using PortalStoque.API.Models.Ocorrencia;
using PortalStoque.API.Models.Usuario;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        static readonly IOcorrenciaRepositorio _OcorRepositorio = new OcorrenciaRepositorio();
        static readonly IUserRepositorio _UserRepositorio = new UserRepositorio();

        public HttpResponseMessage GetAll([FromUri]FilterModel pFilter)
        {
            string userId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var user = _UserRepositorio.GetCurrentUser(userId);

            string filter = WhereClause.GetFilter(pFilter, user);

            List<Ocorrencia> Tasks = _OcorRepositorio.GetAll(filter, pFilter.Pagina, pFilter.TamPag).ToList();
            int TotalOcor = _OcorRepositorio.GetTotalOcor(filter);

            return Request.CreateResponse(HttpStatusCode.OK, new { Tasks, TotalOcor });
        }
    }
}
