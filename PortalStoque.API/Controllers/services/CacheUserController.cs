using PortalStoque.API.Models.Usuarios;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace PortalStoque.API.Controllers.services
{
    public class CacheUserController : ApiController
    {
        static readonly IUsuarioRepositorio _UserRepositorio = new UsuarioRepositorio();

        public Usuario GetUser()
        {
            if (((ClaimsIdentity)User.Identity).Claims.Count() > 0)
            {
                string userId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "UserId").Value;
                return _UserRepositorio.GetCurrentUser(userId);
            }
            return null;
        }
    }
}
