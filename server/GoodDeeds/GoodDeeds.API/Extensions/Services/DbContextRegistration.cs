using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GoodDeeds.Infrastructure;
using GoodDeeds.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GoodDeeds.API.Extensions.Services;

[ExcludeFromCodeCoverage]
public static class DbContextRegistration
{
    public static void AddDbContexts(this IServiceCollection collection, ConfigurationManager config)
    {
        var sqliteConnectionString = config.GetSection("SqliteConnection")["ConnectionString"];

        collection.AddDbContext<GoodDeedsDbContext>(options =>
            options.UseSqlite(sqliteConnectionString, option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            })
        );
    }
}