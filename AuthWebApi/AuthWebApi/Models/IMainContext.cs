using System.Data.Entity;
using System.Threading.Tasks;

namespace AuthWebApi.Models
{
   public interface IMainContext
   {
      IDbEntity<Client> EntityClients { get; set; }
      IDbEntity<RefreshToken> EntityRefreshTokens { get; set; }
      IDbEntity<T> Set<T>() where T : class;
      Task<int> SaveAsync();
   }
}