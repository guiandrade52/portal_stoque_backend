using System;

namespace PortalStoque.API.Models.Ocorrencia
{
    public class FilterModel
    {
        public string SearchMultiple { get; set; }
        public string DateInit { get; set; }
        public string DateFinal { get; set; }
        public string Contrato{ get; set; }
        public string Serie { get; set; }
        public string Servico { get; set; }
        public string UsuarioPortal { get; set; }
        public string ClienteAb { get; set; }
        public string ClienteAt { get; set; }
        public int Pagina { get; set; }
        public int TamPag { get; set; }
    }
}