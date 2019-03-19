using Microsoft.Owin.Security.OAuth;
using PortalStoque.API.Models.Usuarios;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace PortalStoque.API
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IUsuarioRepositorio _UserRepositorio = new UsuarioRepositorio();

            Login login = new Login
            {
                UserName = context.UserName,
                Password = context.Password
            };

            login = _UserRepositorio.Login(login);

            if (login != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("UserId", login.IdUsuario.ToString()));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_login", "Usuário ou senha inválidos");
                return;
            }
        }
    }
}