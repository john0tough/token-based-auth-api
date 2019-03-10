using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApi.Models
{
   public interface IDbEntity<TEntity>  where TEntity : class
   {
      IQueryable<TEntity> GetAll();

      Task<TEntity> FindAsync(params Object[] keyValues);

      TEntity Find(params object[] keyValues);

      TEntity Add(TEntity entity);

      TEntity Remove(TEntity entity);

      TEntity Attach(TEntity entity);

      TEntity Create();
   }
}