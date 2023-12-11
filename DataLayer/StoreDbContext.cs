using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class StoreDbContext : DbContext
    {
        public DbSet<DElement> Elements { get; set; } = null!;
        public DbSet<DProduct> Products { get; set; } = null!;

        public DbSet<DClient> Clients { get; set; } = null!;
        public DbSet<DSale> Sales { get; set; } = null!;

        public DbSet<DTransmutationRecipe> TransmutationRecipes { get; set; } = null!;
        public DbSet<DTransmutation> Transmutations { get; set; } = null!;

        public StoreDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=helloapp456.db");
        }
    }
}
