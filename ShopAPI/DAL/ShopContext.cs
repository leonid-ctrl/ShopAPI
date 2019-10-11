using ShopDbAccess.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ShopDbAccess.DAL
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("ShopContext")
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Merchandise> Merchandise { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}