using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce.Models;

namespace Mini_E_Commerce.Data_Access
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order> OrderItems { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        

    }
    }
