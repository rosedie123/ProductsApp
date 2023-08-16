using Microsoft.EntityFrameworkCore;
using ProductsApp.Models;

namespace ProductsApp.Data
{
    public class DbMainContext:DbContext
    {

        public DbMainContext(){}

        public DbMainContext(DbContextOptions<DbMainContext> options) : base(options) {}

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
       
    }
}
