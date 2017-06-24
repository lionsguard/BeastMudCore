using Beast.Security;
using Beast.Sqlite;
using Beast.Sqlite.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Beast
{
    public static class SqliteBeastBuilderExtensions
    {
        public static IBeastBuilder AddSqlite(this IBeastBuilder builder)
        {
            builder.Services.AddDbContext<SqliteContext>(options => options.UseSqlite("Data Source=beastmud.db"));

            builder.Services.AddTransient<IUserRepository, UserRepository>();

            return builder;
        }
    }
}
