using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AuthWebApi.Models;
using AuthWebApi.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;

namespace AuthWebApi.Providers
{
   public class SimpleAuthorizationServerProvider: OAuthAuthorizationServerProvider
   {
      private readonly IAuthRepository<UserModel, RepoResponse> repoService;
      public SimpleAuthorizationServerProvider(IAuthRepository<UserModel, RepoResponse> repoService)
      {
         this.repoService = repoService;
      }
      public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
      {
         context.Validated();
      }

      public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
      {

         context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

         
            var user = await this.repoService.FindUser(context.UserName, context.Password);

            if (user == null)
            {
               context.SetError("invalid_grant", "The user name or password is incorrect.");
               return;
            }
         

         var identity = new ClaimsIdentity(context.Options.AuthenticationType);
         identity.AddClaim(new Claim("sub", context.UserName));
         identity.AddClaim(new Claim("role", "user"));

         context.Validated(identity);

      }
   }
}