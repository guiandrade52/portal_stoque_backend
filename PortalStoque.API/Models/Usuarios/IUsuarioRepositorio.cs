using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Usuarios
{
    interface IUsuarioRepositorio
    {
        Usuario GetCurrentUser(string UserId);
    }
}