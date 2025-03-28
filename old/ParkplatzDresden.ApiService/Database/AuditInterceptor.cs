using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ParkplatzDresden.ApiService.Models.Database;

namespace ParkplatzDresden.ApiService.Database;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            if (entry.Entity is not BaseEntity baseEntity) continue;
            
            baseEntity.ChangedAt = DateTime.UtcNow;
            
            if (entry.State == EntityState.Added)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
            }
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}