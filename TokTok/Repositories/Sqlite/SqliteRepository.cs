using TokTok.Data;

namespace TokTok.Repositories.Sqlite
{
    public abstract class SqliteRepository
    {
        protected readonly TokTokDbContext Context;

        protected SqliteRepository(TokTokDbContext context)
        {
            Context = context;
        }

        protected virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
