using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class BaseLibraryDbContext : DbContext
    {
        public BaseLibraryDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=/app/db/data.db");
        }
    }
}
