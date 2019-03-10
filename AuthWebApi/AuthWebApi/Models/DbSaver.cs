using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AuthWebApi.Models
{
   public class DbSaver: ISaver
   {
      private readonly IMainContext ctx;

      public DbSaver(IMainContext ctx)
      {
         this.ctx = ctx;
      }


      public async Task<int> AsyncSave()
      {
         return await this.ctx.SaveAsync();
      }
   }
}