using System.Collections.Generic;
using AuthWebApi.Models;

namespace AuthWebApi.Migrations
{
   using System;
   using System.Data.Entity;
   using System.Data.Entity.Migrations;
   using System.Linq;

   internal sealed class Configuration : DbMigrationsConfiguration<AuthWebApi.Models.AuthContext>
   {
      public Configuration()
      {
         AutomaticMigrationsEnabled = false;
      }

      protected override void Seed(AuthWebApi.Models.AuthContext context)
      {
         context.Clients.AddRange(this.BuildClientsList());
      }

      private List<Client> BuildClientsList()
      {

         List<Client> clientsList = new List<Client>
         {
            new Client
            { Id = "ngAuthApp",
               Secret= Helper.Helper.GetHash("abc@123"),
               Name="AngularJS front-end Application",
               ApplicationType =  Models.ApplicationTypes.JavaScript,
               Active = true,
               RefreshTokenLifeTime = 7200,
               AllowedOrigin = "http://localhost:"
            },
            new Client
            { Id = "consoleApp",
               Secret=Helper.Helper.GetHash("123@abc"),
               Name="Console Application",
               ApplicationType =Models.ApplicationTypes.NativeConfidential,
               Active = true,
               RefreshTokenLifeTime = 14400,
               AllowedOrigin = "*"
            }
         };

         return clientsList;
      }
   }
}
