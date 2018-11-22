using PortalStoque.API.Models.Usuarios;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace PortalStoque.API.Controllers.services
{
    public class UsuarioCorrent : ApiController
    {
        static readonly IUsuarioRepositorio _UserRepositorio = new UsuarioRepositorio();

        public Usuario GetUsuario()
        {
            if (((ClaimsIdentity)User.Identity).Claims.Count() > 0)
            {
                int userId = Convert.ToInt32(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                return _UserRepositorio.GetUsuario(userId);
            }
            return null;
        }

        public Permisoes GetPermisoes()
        {
            if (((ClaimsIdentity)User.Identity).Claims.Count() > 0)
            {
                int userId = Convert.ToInt32(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                return _UserRepositorio.GetPermisoes(userId);
            }
            return null;
        }
    }
}
