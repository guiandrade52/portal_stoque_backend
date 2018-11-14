using Microsoft.Owin.Security.OAuth;
using PortalStoque.API.Models.Services;
using PortalStoque.API.Models.Usuario;
using System.Security.Claims;
using System.Threading.Tasks;


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
            Login login = new Login
            {
                UserName = context.UserName,
                Password = context.Password
            };

            if (Auth.AuthService(login))
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

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