using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using AuthWebApi.Models;
using AuthWebApi.Providers;
using AuthWebApi.Repository;
using LightInject;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(AuthWebApi.Startup))]
namespace AuthWebApi
{
   public class Startup
   {
      public void Configuration(IAppBuilder app)
      {
         HttpConfiguration config = new HttpConfiguration(); // aqui se configuran las rutas del API, le pasamos este objeto al WebApiConfig.Register
         WebApiConfig.Register(config); // agrega mas configuraciones al HttpConfig
         SwaggerConfig.Register(config); // activa documentacion swagger

         var container = new ServiceContainer();
         container.RegisterApiControllers();
         container.EnableWebApi(config);


         this.RegisterServices(container);
         this.ConfigureOAuth(app, container);
         app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll); //activa cors
         app.UseWebApi(config); // aqui se enlaza a la pila de ejecucion la API con el middleware OWIN
      }

      public void ConfigureOAuth(IAppBuilder app, IServiceContainer container)
      {
         // Token Generation
         app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
         {
            AllowInsecureHttp = true,
            TokenEndpointPath = new PathString("/token"),
            AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            Provider = new SimpleAuthorizationServerProvider(
               container.GetInstance<IUserRepository<UserModel, RepoResponse>>(),
               container.GetInstance<IRefreshTokenRepository<RefreshToken>>(),
               container.GetInstance<IClient<Client>>()),
            RefreshTokenProvider = new SimpleRefreshTokenProvider(
               container.GetInstance<IRefreshTokenRepository<RefreshToken>>())
         });
         app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

      }

      public void RegisterServices(IServiceContainer container)
      {
         container.Register<DbContext, AuthContext>();
         container.Register<IUserStore<IdentityUser>, UserStore<IdentityUser>>();
         container.Register<UserManager<IdentityUser>>();
         container.Register<IAuthContext, AuthContext>();
         container.Register<IUserRepository<UserModel, RepoResponse>, AuthRepo>();
         container.Register<IRefreshTokenRepository<RefreshToken>, AuthRepo>();
         container.Register<IClient<Client>, AuthRepo>();
      }
   }
}