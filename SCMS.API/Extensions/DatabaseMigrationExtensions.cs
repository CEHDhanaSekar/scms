using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SCMS.API.Extensions;

public static class DatabaseMigrationExtensions
{
    public static void MigrateDatabase<T>(this WebApplication app) where T : DbContext
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<T>();
            context.Database.SetCommandTimeout(600);
            context.Database.Migrate();
        }
    }
}
