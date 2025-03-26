using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ParkplatzDresden.ApiService.Models.Database;

namespace ParkplatzDresden.ApiService.Database;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = new CancellationToken())
    {
        var context = eventData.Context;
        if (context is null)
        {
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity baseEntity)
            {
                switch (entry)
                {
                    case { State: EntityState.Added }:
                        baseEntity.CreatedAt = DateTime.UtcNow;
                        baseEntity.ChangedAt = DateTime.UtcNow;
                        break;
                    case { State: EntityState.Modified }:
                        baseEntity.ChangedAt = DateTime.UtcNow;
                        break;
                }
            }
        }
        
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}