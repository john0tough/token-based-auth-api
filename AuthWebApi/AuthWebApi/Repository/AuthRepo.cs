using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AuthWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthWebApi.Repository
{
   public class AuthRepo : IAuthRepository<UserModel, RepoResponse>
   {
      private UserManager<IdentityUser> userManager;
      public AuthRepo(UserManager<IdentityUser> userManager)
      {
         this.userManager = userManager;
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
   }
}