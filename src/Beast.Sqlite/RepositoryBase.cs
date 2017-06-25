using System;

namespace Beast.Sqlite
{
    public abstract class RepositoryBase : IDisposable
    {
        protected SqliteContext Context { get; private set; }

        protected RepositoryBase(SqliteContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
            Context = null;
        }
    }
}
