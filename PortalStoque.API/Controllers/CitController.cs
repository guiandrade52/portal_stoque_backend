using PortalStoque.API.Models.Cits;
using PortalStoque.API.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Xml;

namespace PortalStoque.API.Controllers
{
    public class CitController : ApiController
    {
        static readonly ICitRepositorio _CitRepositorio = new CitRepositorio();

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
    }
}
