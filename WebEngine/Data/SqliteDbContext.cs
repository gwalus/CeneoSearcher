using Microsoft.EntityFrameworkCore;

namespace WebEngine.Data
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
