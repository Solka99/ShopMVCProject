using Microsoft.EntityFrameworkCore;
using ShopMVCProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ShopMVCProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Clothes", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Food", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Drink", DisplayOrder = 4 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Headphones",
                    Price = 25,
                    CategoryId = 1,
                    ImageUrl = "\\images\\product\\1c7c70b9-55f0-4157-a48d-7851ebe68822.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Coca-Cola",
                    Price = 5,
                    CategoryId = 4,
                    ImageUrl = "\\images\\product\\8d3277c9-0d8f-4c70-8037-f5f7336c56c5.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Banana",
                    Price = 2,
                    CategoryId = 3,
                    ImageUrl = "\\images\\product\\c66dabb3-5704-4061-874d-4caa7a7ffc49.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Chocolate",
                    Price = 4,
                    CategoryId = 3,
                    ImageUrl = "\\images\\product\\7d787ab8-9286-4e0a-bb5c-44ede97e5da8.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Jacket",
                    Price = 30,
                    CategoryId = 2,
                    ImageUrl = "\\images\\product\\72e5fa35-4363-4fb9-a1cd-e5617cc8d26d.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "Shoes",
                    Price = 40,
                    CategoryId = 2,
                    ImageUrl = "\\images\\product\\a2c0bd01-5235-486e-b34f-9b0258f9810f.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "Shoes",
                    Price = 40,
                    CategoryId = 2,
                    ImageUrl = "\\images\\product\\a2c0bd01-5235-486e-b34f-9b0258f9810f.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "Iphone",
                    Price = 150,
                    CategoryId = 1,
                    ImageUrl = "\\images\\product\\85e6cb79-9ffa-4f87-a9b1-835846dd84a6.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "Milk",
                    Price = 3,
                    CategoryId = 3,
                    ImageUrl = "\\images\\product\\108f95d5-00b7-4052-b685-0b0ab16f5fdb.jpg"
                }
                );
            modelBuilder.Entity<ShoppingCart>().HasData(
                new ShoppingCart { Id = 1 },
                new ShoppingCart { Id = 2 },
                new ShoppingCart { Id = 3 }
                );
        }
    }
}