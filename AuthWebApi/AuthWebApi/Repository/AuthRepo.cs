using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuthWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthWebApi.Repository
{
   public class AuthRepo : 
      IUserRepository<UserModel, RepoResponse>,
      IRefreshTokenRepository<RefreshToken>, 
      IClient<Client>
   {
      private readonly UserManager<IdentityUser> userManager;
      private readonly IAuthContext ctx;

      public AuthRepo(UserManager<IdentityUser> userManager, IAuthContext context)
      {
         this.userManager = userManager;
         this.ctx = context;
      }

      public async Task<RepoResponse> RegisterUser(UserModel user)
      {
         var identityUser = new IdentityUser
         {
            UserName = user.UserName
         };
         var result = await this.userManager.CreateAsync(identityUser, user.Password);
         return new RepoResponse
         {
            Errors = result.Errors,
            Succeded = result.Succeeded
         };
      }

      public async Task<UserModel> FindUser(string userName, string password)
      {
         IdentityUser identityUser = await this.userManager.FindAsync(userName, password);
         return identityUser != null ? new UserModel
         {
            UserName = identityUser.UserName,
            Password = password
         } : null;
      }

      public void Dispose()
      {
         this.userManager.Dispose();
      }

      public Client FindClient(string clientId)
      {
         var client = this.ctx.EntityClients.GetAll().FirstOrDefault(c => c.Id == clientId);
         return client;
      }

      public async Task<bool> AddRefreshToken(RefreshToken refreshToken)
      {

         var existingToken = this.ctx.EntityRefreshTokens
            .GetAll()
            .FirstOrDefault(e => e.Id == refreshToken.Id);

         if (existingToken != null)
         {
            var result = await RemoveRefreshToken(existingToken);
         }

         this.ctx.EntityRefreshTokens.Add(refreshToken);

         return await this.ctx.SaveAsync() > 0;
      }

      public async Task<bool> RemoveRefreshToken(string refreshTokenId)
      {
         var result = this.ctx.EntityRefreshTokens
            .GetAll().FirstOrDefault(rt => rt.Id == refreshTokenId);

         if (result != null)
         {
            this.ctx.EntityRefreshTokens.Remove(result);
            return await this.ctx.SaveAsync() > 0;
         }
         return false;
      }

      public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
      {
         this.ctx.EntityRefreshTokens.Remove(refreshToken);
         return await this.ctx.SaveAsync() > 0;
      }

      public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
      {
         var refreshToken =  this.ctx.EntityRefreshTokens.GetAll()
            .FirstOrDefault(rt => rt.Id == refreshTokenId);

         return refreshToken;
      }

      public List<RefreshToken> GetAllRefreshTokens()
      {
         return this.ctx.EntityRefreshTokens.GetAll().ToList();
      }
   }
}