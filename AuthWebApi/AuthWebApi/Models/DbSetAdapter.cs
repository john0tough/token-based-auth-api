using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthWebApi.Models
{
   public class DbSetAdapter<T>: IDbEntity<T> where T : class
   {
      private readonly DbSet<T> privateDbSet;

      public DbSetAdapter(DbSet<T> d)
      {
         this.privateDbSet = d;
      }

      public IQueryable<T> GetAll()
      {
         return this.privateDbSet;
      }

      public async Task<T> FindAsync(params object[] keyValues)
      {
         return await this.privateDbSet.FindAsync(keyValues);
      }

      public T Find(params object[] keyValues)
      {
         return this.privateDbSet.Find(keyValues);
      }

      public T Add(T entity)
      {
         return this.privateDbSet.Add(entity);
      }

      public T Remove(T entity)
      {
         return this.privateDbSet.Remove(entity);
      }

      public T Attach(T entity)
      {
         return this.privateDbSet.Attach(entity);
      }

      public T Create()
      {
         return this.privateDbSet.Create();
      }
   }
}