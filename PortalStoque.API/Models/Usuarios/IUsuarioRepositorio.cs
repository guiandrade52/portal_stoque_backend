using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Usuarios
{
    interface IUsuarioRepositorio
    {
        Usuario GetUsuario(int id);
        Permisoes GetPermisoes(int id);
        Login Login(Login login);
        IEnumerable<Usuario> GetAll(string filter);
    }
}