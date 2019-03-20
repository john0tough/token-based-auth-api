using System.Data.Entity;
using System.Threading.Tasks;

namespace AuthWebApi.Models
{
   public interface IContext
   {
      IDbEntity<T> Set<T>() where T : class;
      Task<int> SaveAsync();
   }
}