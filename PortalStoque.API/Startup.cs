using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(PortalStoque.API.Startup))]

namespace PortalStoque.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // configuracao WebApi
            var config = new HttpConfiguration();


            // configurando rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
             );


            // ativando cors
            app.UseCors(CorsOptions.AllowAll);
            // ativando a geração do token
            AtivarGeracaoTokenAcesso(app);
            // ativando configuração WebApi
            app.UseWebApi(config);
        }

        private void AtivarGeracaoTokenAcesso(IAppBuilder app)
        {
            // Configurando fornecimento de token
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderDeTokensDeAcesso()
            };

            //Ativa o uso de access tokens
            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
