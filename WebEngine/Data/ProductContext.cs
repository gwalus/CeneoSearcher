using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace WebEngine.Data
{
    /// <summary>
    /// Basic database context class to create a database.
    /// </summary>
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
