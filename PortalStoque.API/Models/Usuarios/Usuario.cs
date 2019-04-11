namespace PortalStoque.API.Models.Usuarios
{
    public class Usuario : Login
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int CodContato { get; set; }
        public int CodParc { get; set; }
    }
}