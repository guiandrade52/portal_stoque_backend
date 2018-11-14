using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Usuario
{
    interface IUserRepositorio
    {
        bool ValidaUsuario(string pLogin, string pSenha);
        Usuario GetUsuario(string pLogin, string pSenha);
    }
}