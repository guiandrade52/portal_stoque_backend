﻿namespace PortalStoque.API.Models.Usuarios
{
    public class Login
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public char Ativo { get; set; }
    }
}