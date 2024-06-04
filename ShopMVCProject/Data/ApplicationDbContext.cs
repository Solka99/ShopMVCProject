using Microsoft.EntityFrameworkCore;
using ShopMVCProject.Models;

namespace ShopMVCProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Fortune of Time",
                    Price = 90,
                    CategoryId=1,
                    ImageUrl=""
                },
                new Product
                {
                    Id = 2,
                    Name = "Dark Skies",
                    Price = 30,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Name = "Vanish in the Sunset",
                    Price = 50,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 4,
                    Name = "Cotton Candy",
                    Price = 65,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 5,
                    Name = "Rock in the Ocean",
                    Price = 27,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 6,
                    Name = "Leaves and Wonders",
                    Price = 23,
                    CategoryId = 3,
                    ImageUrl = ""
                }
                );
            modelBuilder.Entity<ShoppingCart>().HasData(
                new ShoppingCart { Id = 1},
                new ShoppingCart { Id = 2},
                new ShoppingCart { Id = 3}
                );
        }
    }
}
