using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Series
{
    public class SerieDetails : Serie
    {
        public int CodParc { get; set; }
        public int CodParcCon { get; set; }
        public int Contrato { get; set; }
        public string Parceiro { get; set; }
        public string ControleFab { get; set; }
        public string DescrNat { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int CodEndereco { get; set; }
        public int CodCidade { get; set; }
        public int CodBairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string DescrGrupo { get; set; }
        public int CodGrupo { get; set; }
        public int CodProduto { get; set; }
    }
}