using PortalStoque.API.Models.Ocorrencias;
using PortalStoque.API.Models.Parceiros;
using PortalStoque.API.Models.Relatorios;
using System.Data;
using System.Net.Http;
using System.Web.Http;
using ClosedXML.Excel;
using System.IO;
using System;
using System.Net;
using System.Net.Http.Headers;

namespace PortalStoque.API.Controllers
{
    public class RelatorioController : ApiController
    {
        static readonly IParceiroRepositorio _parceiroRepositorio = new ParceiroRepositorio();
        static readonly IRelatorioRepositorio _relatorioRepositorio = new RelatorioRepositorio();

        public HttpResponseMessage GetRelatorio([FromUri]Filter pFilter)
        {
            var u = new services.UsuarioCorrent();
            string filter = QueryOcor.GetFilter(pFilter, u.GetPermisoes(), u.GetUsuario());

            Guid guid = Guid.NewGuid();
            string chave = guid.ToString().Replace("-", "");

            using (XLWorkbook wb = new XLWorkbook())
            {
                MemoryStream memoryStream = new MemoryStream();
                DataTable dt = _relatorioRepositorio.GetOcorrencia(filter);
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                wb.SaveAs(memoryStream);
                memoryStream.Position = 0;
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(memoryStream);
                return response;
            }

        }
    }
}
