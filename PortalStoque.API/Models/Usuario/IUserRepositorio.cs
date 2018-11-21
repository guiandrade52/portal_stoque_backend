using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Usuario
{
    interface IUserRepositorio
    {
        UserModel GetCurrentUser(string UserId);
    }
}