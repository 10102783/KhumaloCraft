using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KhumaloCraft.Data
{
    public class ProductContext:IdentityDbContext<AppUser>
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        {

    }

        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<CONTACTUS> CONTACTUS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                 .Property(p => p.Price)
                 .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingCart>()
                .Property(t => t.TotalPrice)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .Property(ta => ta.TotalAmount)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<OrderItem>()
                .Property(u => u.UnitPrice)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);




        }



    }
}
