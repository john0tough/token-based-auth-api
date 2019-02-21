using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AuthWebApi.Models;
using AuthWebApi.Repository;
using Microsoft.AspNet.Identity;

namespace AuthWebApi.Controllers
{
   [RoutePrefix("api/account")]
   public class AccountController : ApiController
   {
      private IAuthRepository<UserModel, RepoResponse> repoService;
      AccountController(IAuthRepository<UserModel, RepoResponse> repoService)
      {
         this.repoService = repoService;
      }

      // POST api/Account/Register
      [AllowAnonymous]
      [Route("Register")]
      public async Task<IHttpActionResult> Register(UserModel userModel)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         RepoResponse result = await this.repoService.RegisterUser(userModel);

         IHttpActionResult errorResult = GetErrorResult(result);

         if (errorResult != null)
         {
            return errorResult;
         }

         return Ok();
      }

      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            this.repoService.Dispose();
         }

         base.Dispose(disposing);
      }

      private IHttpActionResult GetErrorResult(RepoResponse result)
      {
         if (result == null)
         {
            return InternalServerError();
         }

         if (!result.succeded)
         {
            if (result.errors != null)
            {
               foreach (string error in result.errors)
               {
                  ModelState.AddModelError("", error);
               }
            }

            if (ModelState.IsValid)
            {
               // No ModelState errors are available to send, so just return an empty BadRequest.
               return BadRequest();
            }

            return BadRequest(ModelState);
         }

         return null;
      }

   }
}
