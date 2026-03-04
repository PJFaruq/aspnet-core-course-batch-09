using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.DataAccessLayer.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configuring table name
            modelBuilder.Entity<Category>().ToTable("Category");

            //Seeding data
            modelBuilder.Entity<Category>().HasData(
                        new Category { Id = 1, Name = "Electronics", Description = "This is electronics", CreatedDate = new DateTime(2026, 01, 01) },
                        new Category { Id = 2, Name = "Cloths", Description = "This is Clothes", CreatedDate = new DateTime(2026, 01, 01) },
                        new Category { Id = 3, Name = "Books", Description = "This is Books", CreatedDate = new DateTime(2026, 01, 01) }
                );

            modelBuilder.Entity<Product>().ToTable("Product");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
