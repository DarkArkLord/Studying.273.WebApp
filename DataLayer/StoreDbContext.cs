using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class StoreDbContext : DbContext
    {
        public DbSet<DElement> Elements { get; set; } = null!;
        public DbSet<DProduct> Products { get; set; } = null!;
        public DbSet<DBranch> Branches { get; set; } = null!;

        public DbSet<DUser> Users { get; set; } = null!;
        public DbSet<DSale> Sales { get; set; } = null!;
        public DbSet<DSupply> Supplies { get; set; } = null!;

        public DbSet<DTransmutationRecipe> TransmutationRecipes { get; set; } = null!;
        public DbSet<DTransmutation> Transmutations { get; set; } = null!;

        public StoreDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\data.db");
        }
    }
}
