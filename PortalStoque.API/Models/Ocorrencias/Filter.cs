namespace PortalStoque.API.Models.Ocorrencias
{
    public class Filter
    {
        public string Search { get; set; }
        public string DateInit { get; set; }
        public string DateFinal { get; set; }
        public string Contrato{ get; set; }
        public string Contato { get; set; }
        public string Serie { get; set; }
        public string Servico { get; set; }
        public string UsuarioPortal { get; set; }
        public string ParceiroAb { get; set; }
        public string ParceiroAt { get; set; }
        public int ActivePage { get; set; }
        public int TamPage { get; set; }
    }
}