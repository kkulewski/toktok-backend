using Microsoft.EntityFrameworkCore;
using TokTok.Models;

namespace TokTok.Data
{
    public class TokTokDbContext : DbContext
    {
        public TokTokDbContext()
        {
        }

        public TokTokDbContext(DbContextOptions<TokTokDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=toktok.db");
        }
    }
}
