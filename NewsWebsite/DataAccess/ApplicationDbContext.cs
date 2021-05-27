using Microsoft.EntityFrameworkCore;
using NewsWebsite.DataAccess.Entities;

namespace NewsWebsite.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<News> News { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
