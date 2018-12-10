using TokTok.Database;

namespace TokTok.Repositories.Sqlite
{
    public abstract class SqliteRepository
    {
        protected readonly SqliteDbContext Context;

        protected SqliteRepository(SqliteDbContext context)
        {
            Context = context;
        }

        protected virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
