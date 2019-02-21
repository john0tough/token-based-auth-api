using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthWebApi.Models
{
   public class AuthContext: IdentityDbContext // permite usar la implenetacion de  .net identity  y entity framework , lo que permite usar con code first approach
   {
      public AuthContext() : base("AuthContext")
      {

      }
   }
}