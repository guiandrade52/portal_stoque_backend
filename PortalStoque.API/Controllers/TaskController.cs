using PortalStoque.API.Models.Anexos;
using PortalStoque.API.Models.Cits;
using PortalStoque.API.Models.OcorNews;
using PortalStoque.API.Models.Ocorrencias;
using PortalStoque.API.Models.Rats;
using PortalStoque.API.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        static readonly IOcorrenciaRepositorio _OcorRepositorio = new OcorrenciaRepositorio();
        static readonly ICitRepositorio _CitRepositorio = new CitRepositorio();
        static readonly IAnexoRepositorio _AnexoRepositorio = new AnexoRepositorio();
        static readonly IRatRepositorio _RatRepositorio = new RatRepositorio();
        static readonly IOcorNewsRepositorio _OcorNewsRepositorio = new OcorNewsRepositorio();

        public HttpResponseMessage GetAll([FromUri]Filter pFilter)
        {
            var u = new services.UsuarioCorrent();
            string filter = QueryOcor.GetFilter(pFilter, u.GetPermisoes(), u.GetUsuario());

            List<Ocorrencia> tasks = _OcorRepositorio.GetAll(filter, pFilter.ActivePage, pFilter.TamPage).ToList();
            int totalOcor = _OcorRepositorio.GetTotalOcor(filter);

            foreach (var item in tasks)
            {
                item.Rat = _RatRepositorio.GetDataRat(item.ExecutionId);
                item.Cit = _CitRepositorio.GetCit(item.ExecutionId);
                item.Anexos = _AnexoRepositorio.GetAnexo(item.ExecutionId);
            }

            // Carrega quantidade de ocorrências
            var difQuantPaginas = (totalOcor % pFilter.TamPage) > 0 ? 1 : 0;
            var totalPages = (totalOcor / pFilter.TamPage) + difQuantPaginas;
            return Request.CreateResponse(HttpStatusCode.OK, new { tasks, totalOcor, totalPages });
        }

        [HttpPost]
        public HttpResponseMessage SavedTask(Ocor ocorrencia)
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();

            if (user.RgtOcorrencia == 'S')
            {
                ocorrencia.IdUsuarioPortal = user.IdUsuario;
                string jsonRequestSave = _OcorNewsRepositorio.GetJsonFormattedSave(ocorrencia);
                string json = Ocor.Instance.call("ProcessoSP.iniciaProcesso", "bhbpmsnkbpms", jsonRequestSave);
                json = _OcorNewsRepositorio.GetNuOcorrencia(json);

                if (json.Split(':').Length == 4)
                {
                    string executionid = json.Split(':')[3].Replace("}", "").Replace("'", "").Replace("\"", "");

                    if (int.TryParse(executionid, out int executionidnumber))
                        _OcorNewsRepositorio.Update("UPDATE AD_STOOCO SET DESCRICAO = '{0}' WHERE EXECUTIONID = {1}".Replace("{0}", ocorrencia.Descricao.Replace("'", "\"")).Replace("{1}", executionid));
                    else
                        return Request.CreateResponse(HttpStatusCode.BadRequest, _OcorNewsRepositorio.Message(executionid));
                }
                else
                    _OcorNewsRepositorio.Update("UPDATE AD_STOOCO SET DESCRICAO = '{0}' WHERE EXECUTIONID = {1}".Replace("{0}", ocorrencia.Descricao.Replace("'", "\"")).Replace("{1}", json));

                return Request.CreateResponse(HttpStatusCode.Created, json);
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, "O usuário não possui autorização para abrir uma ocorrencia.");
        }

        [HttpPost]
        [Route("UploadFile")]
        public HttpResponseMessage MediaUpload(string executionId)
        {
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count < 1)
                return Request.CreateResponse(HttpStatusCode.NoContent);

            try
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    HttpPostedFile arquivo = httpRequest.Files[i];
                    Guid guid = Guid.NewGuid();
                    string chave = guid.ToString().Replace("-", "").ToLower();
                    var uploadPath = HttpContext.Current.Server.MapPath("~/Temp/Anexos");
                    string caminhoArquivo = Path.Combine(@uploadPath, chave);
                    arquivo.SaveAs(caminhoArquivo);

                    DataTable dt = DBManager.GetDataTable("SELECT MAX(NUATTACH)+1 FROM TSIANX");
                    DBManager.Insert(DBManager.GetFormatedQuery(Convert.ToString(dt.Rows[0][0]), chave, arquivo.FileName, executionId + "_0_BHBPMAtividade"));
                    AnexoService.TransferFile(HttpContext.Current.Server.MapPath("~/Services/") + "WinSCP.exe", caminhoArquivo);
                    File.Delete(caminhoArquivo);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
