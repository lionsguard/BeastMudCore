using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Beast.Sqlite
{
    public class SqliteContextFactory : IDbContextFactory<SqliteContext>
    {
        public SqliteContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<SqliteContext>();
            builder.UseSqlite("Data Source=beastmud.db");
            return new SqliteContext(builder.Options);
        }
    }
}
