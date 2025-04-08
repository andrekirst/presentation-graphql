using ApiService.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Infrastructure;

public class MigrateDatabaseService(IDbContextFactory<AppDbContext> dbContextFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var dbContext = await dbContextFactory.CreateDbContextAsync(stoppingToken);
        await dbContext.Database.MigrateAsync(stoppingToken);
    }
}