using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Contratos
{
    public class ContratoDetails : Contrato
    {
        public string Cep { get; set; }
        public int CodBairro { get; set; }
        public int CodCidade { get; set; }
        public int CodEndereco { get; set; }
        public int CodParcCon { get; set; }
        public string Complemento { get; set; }
        public string DescrNat { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Contrato { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
    }
}