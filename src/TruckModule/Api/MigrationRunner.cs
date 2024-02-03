using ErpApp.TruckModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ErpApp.TruckModule.Api;

public static class MigrationRunner
{
    public static async Task<IHost> RunMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TruckDbContext>();

        await dbContext.Database.MigrateAsync();

        return host;
    }
}