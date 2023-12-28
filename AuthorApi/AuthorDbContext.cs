using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthorApi
{
    public class AuthorDbContext : BaseLibraryDbContext
    {
        public DbSet<DAuthor> Authors { get; set; } = null!;
    }
}
