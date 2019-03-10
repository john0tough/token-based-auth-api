using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthWebApi.Models
{
   public class AuthContext: IdentityDbContext, IMainContext // permite usar la implenetacion de  .net identity  y entity framework , lo que permite usar con code first approach
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
         return await this.SaveChangesAsync();
      }
   }
}