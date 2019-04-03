using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Contratos
{
    public class CadContrato
    {
        public int IdUsuario { get; set; }
        public int Contrato { get; set; }
        public int CodParc { get; set; }
        public string Nome { get; set; }
    }
}