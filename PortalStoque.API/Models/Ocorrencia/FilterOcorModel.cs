using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Ocorrencia
{
    public class FilterOcorModel
    {
        public int ExecutionId { get; set; }
        public DateTime DataCr { get; set; }
        public DateTime DataTs { get; set; }
        public int Contrato{ get; set; }
        public string Serie { get; set; }
        public string Servico { get; set; }
        public int ContratoResponsavel { get; set; }
        public int UsuarioPortal { get; set; }
        public int ClienteAb { get; set; }
        public int ClienteAt { get; set; }
        
    }
}