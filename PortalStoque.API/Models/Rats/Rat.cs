using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Rats
{
    public class Rat
    {
        public int ExecutionId { get; set; }
        public int NumeroVisita { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public string Defeito { get; set; }
        public string Causa { get; set; }
        public string Solucao { get; set; }
    }
}