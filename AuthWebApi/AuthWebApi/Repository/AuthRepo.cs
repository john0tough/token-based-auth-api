using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuthWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthWebApi.Repository
{
   public class AuthRepo : IAuthRepository<UserModel, RepoResponse>, 
      IRefreshTokenRepository
   {
      private readonly UserManager<IdentityUser> userManager;
      private readonly IMainContext ctx;

      public AuthRepo(UserManager<IdentityUser> userManager, IMainContext context)
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
            errors = result.Errors,
            succeded = result.Succeeded
         };
      }

      public async Task<UserModel> FindUser(string userName, string password)
      {
         IdentityUser identityUser = await this.userManager.FindAsync(userName, password);
         return new UserModel
         {
            UserName = identityUser.UserName,
            Password = password
         };
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

         var existingToken = await this.ctx.EntityRefreshTokens.FindAsync(refreshToken);

         if (existingToken != null)
         {
            var result = await RemoveRefreshToken(existingToken);
         }

         this.ctx.EntityRefreshTokens.Add(refreshToken);

         return await this.ctx.SaveAsync() > 0;
      }

      public async Task<bool> RemoveRefreshToken(string refreshTokenId)
      {
         var result = await this.ctx.EntityRefreshTokens
            .GetAll().FirstOrDefaultAsync(rt => rt.Id == refreshTokenId);

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
         var refreshToken = await this.ctx.EntityRefreshTokens.GetAll()
            .FirstOrDefaultAsync(rt => rt.Id == refreshTokenId);

         return refreshToken;
      }

      public List<RefreshToken> GetAllRefreshTokens()
      {
         return this.ctx.EntityRefreshTokens.GetAll().ToList();
      }
   }
}