using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace WebEngine.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
