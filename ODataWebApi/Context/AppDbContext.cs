using Microsoft.EntityFrameworkCore;
using ODataWebApi.Models;

namespace ODataWebApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        // burada küsüratı money yaptığımızda virgülden sonra 4 taneye kadar veri verecektir.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.Property(p => p.Price).HasColumnType("money");
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.Property(p => p.UserType).HasConversion(t => t.Value, v => UserType.FromValue(v));

                builder.OwnsOne(p => p.Adress);
            });
        }
    }
}
