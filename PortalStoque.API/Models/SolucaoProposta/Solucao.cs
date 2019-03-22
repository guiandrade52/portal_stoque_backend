using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.SolucaoProposta
{
    public class Solucao
    {
        public string DataInicio { get; set; }
        public string DataFinal { get; set; }
        public int ExecutionId { get; set; }
        public string SolucaoAplicada { get; set; }
        public int ActivitId { get; set; }
    }
}