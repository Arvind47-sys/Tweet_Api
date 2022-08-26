using Microsoft.EntityFrameworkCore;
using Tweet_Api.Entities;

namespace Tweet_Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Replies> Replies { get; set; }
    }
}