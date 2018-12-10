using Microsoft.EntityFrameworkCore;
using TokTok.Models;

namespace TokTok.Database
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext()
        {
        }

        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
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
