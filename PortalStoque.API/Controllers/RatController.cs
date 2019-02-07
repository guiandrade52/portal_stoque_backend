using PortalStoque.API.Models.Rats;
using PortalStoque.API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace PortalStoque.API.Controllers
{
    public class RatController : ApiController
    {
        static readonly IRatRepositorio _RatRepositorio = new RatRepositorio();

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
                    catch (Exception e)
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
    }
}
