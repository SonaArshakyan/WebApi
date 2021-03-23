using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Provider;

[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/sonatoken"),
                Provider = new WebApiProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };
            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());            
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
