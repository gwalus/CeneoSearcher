using Microsoft.EntityFrameworkCore;
using WebEngine.Entities;

namespace WebEngine.Data
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
