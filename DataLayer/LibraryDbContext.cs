using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<DAuthor> Authors { get; set; } = null!;
        public DbSet<DBookSeries> BookSeries { get; set; } = null!;
        public DbSet<DBook> Books { get; set; } = null!;

        public DbSet<DBranch> Branches { get; set; } = null!;
        public DbSet<DLibrarian> Librarians { get; set; } = null!;
        public DbSet<DClient> Clients { get; set; } = null!;

        public DbSet<DBookRent> BookRents { get; set; } = null!;

        public LibraryDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sqlserver;Database=main_db;User Id=sa;Password=123456789qQ;Encrypt=False;TrustServerCertificate=True;");
        }
    }
}
