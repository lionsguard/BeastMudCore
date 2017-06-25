using Beast.Objects;
using Beast.Security;
using Beast.Sqlite;
using Beast.Sqlite.Objects;
using Beast.Sqlite.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Beast
{
    public static class SqliteBeastBuilderExtensions
    {
        public static IBeastBuilder AddSqlite(this IBeastBuilder builder)
        {
            return builder.AddSqlite(options => options.UseSqlite("Data Source=beastmud.db"));
        }

        public static IBeastBuilder AddSqlite(this IBeastBuilder builder, Action<DbContextOptionsBuilder> optionsAction)
        {
            builder.Services.AddDbContext<SqliteContext>(optionsAction);
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IObjectRepository, ObjectRepository>();

            return builder;
        }
    }
}
