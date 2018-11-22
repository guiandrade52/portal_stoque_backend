using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Usuarios
{
    public class Permisoes
    {
        public char AltPassword { get; set; }
        public string ClienteAb { get; set; }
        public string ClienteAt { get; set; }
        public string NumContrato { get; set; }
        public char RgtOcorrencia { get; set; }
        public string Perfil { get; set; }
    }
}