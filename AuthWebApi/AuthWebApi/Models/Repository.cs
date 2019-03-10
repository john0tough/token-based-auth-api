﻿using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApi.Models
{
   public class Repository<T> : IRepository<T> where T : class
   {
      private readonly IMainContext ctx;

      public Repository(IMainContext ctx)
      {
         this.ctx = ctx;
      }

      public IQueryable<T> GetAll()
      {
         return this.ctx.Set<T>().GetAll();
      }
      
      public T Get(params object[] keyValues)
      {
         return this.ctx.Set<T>().Find(keyValues);
      }

      public async Task<T> GetAsync(params object[] keyValues)
      {
         return await this.ctx.Set<T>().FindAsync(keyValues);
      }

      public T Add(T entity)
      {
         return this.ctx.Set<T>().Add(entity);
      }

      public T Remove(T entity)
      {
         return this.ctx.Set<T>().Remove(entity);
      }
   }
}