using Microsoft.EntityFrameworkCore;

namespace WebApiUtils.BaseApi
{
    internal class BaseContext<T> : DbContext
        where T : DEntityWithId
    {
        private string connectionString;

        public DbSet<T> Items { get; set; } = null!;

        public BaseContext(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
