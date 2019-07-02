using PortalStoque.API.Models.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ChartsController : ApiController
    {
        static readonly IChartsRepositorio _ChartsRepositorio = new ChartsRepositorio();
        public class ChartLine
        {
            public int Ano { get; set; }
            public int Mes { get; set; }
            public int Total { get; set; }
        };

        public class ChartRound
        {
            public int TotalAbertas { get; set; }
            public int TotalFechadas { get; set; }
        }

        [HttpGet]
        public HttpResponseMessage Charts()
        {
            var u = new services.UsuarioCorrent();
            var permisoes = u.GetPermisoes();
            var user = u.GetUsuario();

            if (string.IsNullOrEmpty(permisoes.Contratos))
                return Request.CreateResponse(HttpStatusCode.NoContent);

            DateTime dtinicial = DateTime.Now;
            List<ChartLine> data = new List<ChartLine>();
            for (int i = 0; i < 12; i++)
            {
                dtinicial = new DateTime(dtinicial.Year, dtinicial.Month, 1).AddMonths(-1);
                if(i == 0)
                    dtinicial = new DateTime(dtinicial.Year, dtinicial.Month, 1).AddMonths(+1);

                DateTime dtfinal = dtinicial.Month != 12
                    ? new DateTime(dtinicial.Year, dtinicial.Month + 1, 1).AddDays(-1)
                    : new DateTime(dtinicial.Year + 1, dtinicial.Month - 11, 1).AddDays(-1);

                Charts chart = new Charts
                {
                    DtInicio = string.Format("{0}/{1}/{2}", dtinicial.Day, dtinicial.Month, dtinicial.Year),
                    DtFinal = string.Format("{0}/{1}/{2}", dtfinal.Day, dtfinal.Month, dtfinal.Year),
                    CodContato = user.CodContato,
                    Contratos = permisoes.Contratos
                };

                data.Add(new ChartLine { Mes = dtinicial.Month, Ano = dtinicial.Year, Total = _ChartsRepositorio.GetChartLine(chart) });
            }

            Charts charts = new Charts
            {
                CodContato = user.CodContato,
                Contratos = permisoes.Contratos,
                Situacao = "AND OCO.SITUACAO IN (19,23,15,1,3,10,4,14,7)"
            };

            int totalAberto = _ChartsRepositorio.GetChartRound(charts);
            charts.Situacao = "AND OCO.SITUACAO IN (8,6,9,21,20,22,2,17,18,16,13)";
            int totalFechadas = _ChartsRepositorio.GetChartRound(charts);

            return Request.CreateResponse(HttpStatusCode.OK, new { ChartsLine = data, ChartsRound = new ChartRound{ TotalFechadas = totalFechadas, TotalAbertas= totalAberto} });
        }
    }
}
