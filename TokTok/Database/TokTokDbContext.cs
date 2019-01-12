using Microsoft.EntityFrameworkCore;
using TokTok.Models;

namespace TokTok.Database
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<UserInChannel> UserInChannels { get; set; }

        public SqliteDbContext()
        {
        }

        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=toktok.db");
        }
    }
}
