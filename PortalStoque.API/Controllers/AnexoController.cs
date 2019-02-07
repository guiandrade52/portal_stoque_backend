using PortalStoque.API.Models.Anexos;
using PortalStoque.API.Services;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    public class AnexoController : ApiController
    {
        static readonly IAnexoRepositorio _AnexoRepositorio = new AnexoRepositorio();

        [Route("Task/Anexo")]
        [HttpGet]
        public HttpResponseMessage Anexo(string chave, string tipo)
        {
            string nomeView = string.Format("{0}.{1}", chave, tipo);
            string pathDownload = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Temp/Anexos/{0}", nomeView));
            string pathExecutable = System.Web.Hosting.HostingEnvironment.MapPath("~/Services/WinSCP.exe");

            if (!File.Exists(pathDownload))
                if (AnexoService.GetFile(pathExecutable, pathDownload, chave))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Anexo não foi encontrado, gentileza entrar em contato com suporte.");
            else
                return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
