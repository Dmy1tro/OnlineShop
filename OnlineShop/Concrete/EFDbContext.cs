using OnlineShop.Entities;
using OnlineShop.SampleData;
using System.Data.Entity;

namespace OnlineShop.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("ShopDatabaseContext")
        {
            this.Configuration.LazyLoadingEnabled = false;

            this.Database.CreateIfNotExists();
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}