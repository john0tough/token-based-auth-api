using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApi.Models
{
   public interface IRepository<TEntity> where TEntity : class
   {
      IEnumerable<TEntity> GetAll();
      TEntity Get(params object[] keyValues);
      Task<TEntity> GetAsync(params Object[] keyValues);
      TEntity Add(TEntity entity);
      TEntity Remove(TEntity entity);
   }
}