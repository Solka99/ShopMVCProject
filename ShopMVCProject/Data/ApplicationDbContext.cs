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
    }
}
