using PortalStoque.API.Models.Anexos;
using PortalStoque.API.Models.Cits;
using PortalStoque.API.Models.Ocorrencias;
using PortalStoque.API.Models.Rats;
using PortalStoque.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;

namespace PortalStoque.API.Controllers
{

    public class TaskController : ApiController
    {
        static readonly IOcorrenciaRepositorio _OcorRepositorio = new OcorrenciaRepositorio();
        static readonly IRatRepositorio _RatRepositorio = new RatRepositorio();
        static readonly ICitRepositorio _CitRepositorio = new CitRepositorio();
        static readonly IAnexoRepositorio _AnexoRepositorio = new AnexoRepositorio();

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

        [Route("Task/Rat")]
        [HttpGet]
        public HttpResponseMessage Rat(int executionId, int visita)
        {
            int i = 1;
            List<int> visitaError = new List<int>();
            while (i <= visita)
            {
                string pathRat = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Temp/RAT_{0}_{1}.pdf", executionId, i));

                if (!File.Exists(pathRat))
                {
                    IRatRepositorio _ratRepositorio = new RatRepositorio();
                    ServiceSankhya.Service.pathcreatefile = System.Web.Hosting.HostingEnvironment.MapPath("~/Temp/");
                    string body = _ratRepositorio.GetRATXML(executionId, i);
                    body = body.Replace("'", "\"");
                    ServiceSankhya.Service.nuocor = Convert.ToString(executionId);
                    ServiceSankhya.Service.nuvisita = Convert.ToString(i);
                    try
                    {
                        XmlDocument doc = ServiceSankhya.Service.call("report.bpms", "bhbpmsnkbpms", body);
                    }
                    catch (Exception)
                    {
                        if (i == visita)
                            visitaError.Add(i);
                        else
                            visitaError.Add(i);
                        i++;
                    }
                }
                else
                {
                    i++;
                }
            }
            if (visitaError.Count > 0)
                return Request.CreateResponse(HttpStatusCode.PartialContent, visitaError);
            else
                return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("Task/Cit")]
        [HttpGet]
        public HttpResponseMessage Cit(int executionId)
        {
            if (executionId == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Número da Visita e ExecutionId são obrigatórios.");

            string path = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Temp/CIT_{0}.pdf", executionId));
            ServiceSankhya.Service.pathcreatefile = System.Web.Hosting.HostingEnvironment.MapPath("~/Temp/");

            if (!File.Exists(path))
            {
                string body = string.Format(@"<relatorio nuRfe='115'>
                                                 <parametros>
                                                      <parametro nome='PK_NO' descricao='Ocorrencia' classe='java.math.BigDecimal' instancia='' valor='{0}' pesquisa='false' requerido='false'/>                                                     
                                                 </parametros>
                                                 </relatorio>", executionId);
                body = body.Replace("'", "\"");
                ServiceSankhya.Service.nuocor = Convert.ToString(executionId);
                XmlDocument doc = ServiceSankhya.Service.call("report.bpms", "bhbpmsnkbpms", body);
                path = doc.FirstChild.InnerText;
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fileStream = File.OpenRead(path);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;

        }

        [Route("Task/Anexo")]
        [HttpGet]
        public HttpResponseMessage Anexo(string chave, string tipo)
        {
            string nomeView = string.Format("{0}.{1}", chave, tipo);
            string pathDownload = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Temp/Anexos/{0}", nomeView));
            string pathExecutable = System.Web.Hosting.HostingEnvironment.MapPath("~/Services/WinSCP.exe");

            if (!File.Exists(pathDownload))
                if (ServiceAnexo.GetFile(pathExecutable, pathDownload, chave))
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Anexo não foi encontrado, gentileza entrar em contato com suporte.");
            else
                return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
