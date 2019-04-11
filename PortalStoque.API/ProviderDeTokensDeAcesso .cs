using Microsoft.Owin.Security.OAuth;
using PortalStoque.API.Models.Usuarios;
using PortalStoque.API.Services;
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

            Login result = _UserRepositorio.Login(login);

            if (result == null)
            {
                CriptoMD5 mD5 = new CriptoMD5();
                login.Password = mD5.RetornarMD5(login.Password);
                result = _UserRepositorio.Login(login);
            }

            if (result != null)
            {
                if(result.Ativo == 'N')
                {
                    context.SetError("invalid_login", "Seu usuário encontra-se desativado, para mais informações entre em contato com suporte.");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("UserId", result.IdUsuario.ToString()));
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