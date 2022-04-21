using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Models
{
    public class ProductsContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
