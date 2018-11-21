using Microsoft.Owin.Security.OAuth;
using PortalStoque.API.Models.Usuario;
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
            UserModel _user = UserRepositorio.GetUsuario(context.UserName, context.Password);

            if (_user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("UserId", _user.IdUsuario.ToString()));
                HttpContext.Current.Cache.Add("UserId=" + _user.IdUsuario, _user, null, DateTime.Now.AddHours(60), Cache.NoSlidingExpiration, CacheItemPriority.High, null);

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Usuário ou senha inválidos");
                return;
            }
        }
    }
}