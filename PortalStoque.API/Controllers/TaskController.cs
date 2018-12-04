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
using System.Web.Http;
using System.Xml;

namespace PortalStoque.API.Controllers
{

    public class TaskController : ApiController
    {
        static readonly IOcorrenciaRepositorio _OcorRepositorio = new OcorrenciaRepositorio();
        static readonly IRatRepositorio _RatRepositorio = new RatRepositorio();
        static readonly ICitRepositorio _CitRepositorio = new CitRepositorio();

        public HttpResponseMessage GetAll([FromUri]Filter pFilter)
        {
            var u = new services.UsuarioCorrent();
            string filter = QueryOcor.GetFilter(pFilter, u.GetPermisoes(), u.GetUsuario());

            List<Ocorrencia> Tasks = _OcorRepositorio.GetAll(filter, pFilter.Pagina, pFilter.TamPag).ToList();
            int TotalOcor = _OcorRepositorio.GetTotalOcor(filter);

            foreach (var item in Tasks)
            {
                item.Rat = _RatRepositorio.GetDataRat(item.ExecutionId);
                item.Cit = _CitRepositorio.GetCit(item.ExecutionId);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { Tasks, TotalOcor });
        }

        [Route("Task/Rat")]
        [HttpGet]
        public HttpResponseMessage Rat(int executionId, int visita)
        {
            if (visita == 0 || executionId == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Número da Visita e ExecutionId são obrigatórios.");

            string path = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Anexos/RAT_{0}_{1}.pdf", executionId, visita));

            if (!File.Exists(path))
            {
                IRatRepositorio _ratRepositorio = new RatRepositorio();

                ServiceSankhya.Service.pathcreatefile = System.Web.Hosting.HostingEnvironment.MapPath("~/Anexos/");
                string body = _ratRepositorio.GetRATXML(executionId, visita);
                body = body.Replace("'", "\"");
                ServiceSankhya.Service.nuocor = Convert.ToString(executionId);
                ServiceSankhya.Service.nuvisita = Convert.ToString(visita);


                XmlDocument doc = ServiceSankhya.Service.call("report.bpms", "bhbpmsnkbpms", body);

                path = doc.FirstChild.InnerText;
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fileStream = File.OpenRead(path);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }

        [Route("Task/Cit")]
        [HttpGet]
        public HttpResponseMessage Cit(int executionId)
        {
            if (executionId == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Número da Visita e ExecutionId são obrigatórios.");

            string path = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Anexos/CIT_{0}.pdf", executionId));

            if (!File.Exists(path))
            {
                string body = string.Format(@"<relatorio nuRfe='115'>
                                                 <parametros>
                                                      <parametro nome='PK_NO' descricao='Ocorrencia' classe='java.math.BigDecimal' instancia='' valor='{0}' pesquisa='false' requerido='false'/>                                                     
                                                 </parametros>
                                                 </relatorio>", executionId);
                body = body.Replace("'", "\"");
                ServiceSankhya.Service.pathcreatefile = System.Web.Hosting.HostingEnvironment.MapPath("~/Anexos/");
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
    }
}
