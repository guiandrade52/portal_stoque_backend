using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Ocorrencia
{
    public class FilterOcorModel
    {
        public int ExecutionId { get; set; }
        public string Serie { get; set; }
        public int Pagina { get; set; }
        public int TamPag { get; set; }

    }
}