using System;
using Owin;
using Microsoft.Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Cors;
using TCN.WebAPI.Providers;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using TCN.WebAPI.App_Start;

[assembly: OwinStartup(typeof(TCN.WebAPI.Startup))]

namespace TCN.WebAPI
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }


        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            WebApiConfig.Register(config);

            app.UseCors(CorsOptions.AllowAll);

            app.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            app.UseNinjectWebApi(config);
        }


        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                Provider = new AuthorizationProvider(),
                RefreshTokenProvider = new RefreshTokenProvider(),
                AllowInsecureHttp = true
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);
        }
    }
}
