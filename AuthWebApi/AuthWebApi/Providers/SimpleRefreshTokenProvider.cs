using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AuthWebApi.Models;
using AuthWebApi.Repository;
using Microsoft.Owin.Security.Infrastructure;

namespace AuthWebApi.Providers
{
   public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
   {
      private readonly IRefreshTokenRepository<RefreshToken> repo;
      public SimpleRefreshTokenProvider(IRefreshTokenRepository<RefreshToken> repo)
      {
         this.repo = repo;
      }

      public void Create(AuthenticationTokenCreateContext context)
      {
         throw new NotImplementedException();
      }

      public async Task CreateAsync(AuthenticationTokenCreateContext context)
      {
         var clientId = context.Ticket.Properties.Dictionary["as:client_id"];

         if (string.IsNullOrEmpty(clientId))
         {
            return;
         }

         var refreshTokenId = Guid.NewGuid().ToString("n");

         var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

         var token = new RefreshToken()
         {
            Id = Helper.Helper.GetHash(refreshTokenId),
            ClientId = clientId,
            Subject = context.Ticket.Identity.Name,
            IssuedUtc = DateTime.UtcNow,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
         };

         context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
         context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

         token.ProtectedTicket = context.SerializeTicket();

         var result = await this.repo.AddRefreshToken(token);

         if (result)
         {
            context.SetToken(refreshTokenId);
         }
      }

      public void Receive(AuthenticationTokenReceiveContext context)
      {
         throw new NotImplementedException();
      }

      public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
      {
         var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
         context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

         string hashedTokenId = Helper.Helper.GetHash(context.Token);

         var refreshToken = await this.repo.FindRefreshToken(hashedTokenId);

         if (refreshToken != null)
         {
            //Get protectedTicket from refreshToken class
            context.DeserializeTicket(refreshToken.ProtectedTicket);
            var result = await this.repo.RemoveRefreshToken(hashedTokenId);
         }
      }
   }
}