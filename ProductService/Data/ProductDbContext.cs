using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 1, Name = "Caño", Price = 1522, Quantity = 30});

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 2, Name = "Proa", Price = 150, Quantity = 20 });

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel { Id = 3, Name = "Ropero", Price = 1500, Quantity = 10 });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductModel> Products { get; set; }
    }
}
