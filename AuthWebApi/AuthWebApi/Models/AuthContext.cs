using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthWebApi.Models
{
   public class AuthContext: IdentityDbContext, IAuthContext // permite usar la implenetacion de  .net identity  y entity framework , lo que permite usar con code first approach
   {
      public AuthContext() : base("AuthContext")
      { }

      public DbSet<Client> Clients { get; set; }
      public DbSet<RefreshToken> RefreshTokens { get; set; }

      public IDbEntity<Client> EntityClients
      {
         get => new DbSetAdapter<Client>(Clients);
         set { }
      }

      public IDbEntity<RefreshToken> EntityRefreshTokens
      {
         get => new DbSetAdapter<RefreshToken>(RefreshTokens);
         set { }
      }

      public new IDbEntity<T> Set<T>() where T : class
      {
         return new DbSetAdapter<T>(base.Set<T>());
      }

      public async Task<int> SaveAsync()
      {
         try
         {
            // Your code...
            // Could also be before try if you know the exception occurs in SaveChanges

            return await this.SaveChangesAsync();
         }
         catch (DbEntityValidationException e)
         {
            foreach (var eve in e.EntityValidationErrors)
            {
               Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                  eve.Entry.Entity.GetType().Name, eve.Entry.State);
               foreach (var ve in eve.ValidationErrors)
               {
                  Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                     ve.PropertyName, ve.ErrorMessage);
               }
            }
            throw;
         }
         
      }
   }
}