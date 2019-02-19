using System.Data.Entity;

namespace AttrRoutingAPI.Models
{
    public class AttrRoutingAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AttrRoutingAPIContext() : base("name=AttrRoutingAPIContext")
        {
        }

      public System.Data.Entity.DbSet<AttrRoutingAPI.Models.Book> Books { get; set; }

      public System.Data.Entity.DbSet<AttrRoutingAPI.Models.Author> Authors { get; set; }
   }
}
