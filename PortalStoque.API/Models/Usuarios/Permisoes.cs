namespace PortalStoque.API.Models.Usuarios
{
    public class Permisoes : Usuario
    {
        public char AltPassword { get; set; }
        public string ClienteAb { get; set; }
        public string ClienteAt { get; set; }
        public string Contratos { get; set; }
        public char RgtOcorrencia { get; set; }
        public string Perfil { get; set; }
        public char ClienteInterno { get; set; }
        public int PassCript { get; set; }
    }
}