using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Anexos
{
    public class Anexo
    {
        public string Nome { get; set; }
        public string Chave { get; set; }
        public string DataCadastro { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
    }
}