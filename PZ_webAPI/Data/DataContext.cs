using Microsoft.EntityFrameworkCore;
using PZ_webAPI.Models;

namespace PZ_webAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(us => new {us.UserID});
            modelBuilder.Entity<Orders>()
                .HasKey(ord => ord.OrderID);
            modelBuilder.Entity<Products>()
                .HasKey(prod => new { prod.ProductID });
        }
    }
}
