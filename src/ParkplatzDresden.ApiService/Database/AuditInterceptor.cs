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

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry)
            {
                case { State: EntityState.Added }:
                    entry.Property(p => p.CreatedAt).CurrentValue = DateTime.UtcNow;
                    entry.Property(p => p.ChangedAt).CurrentValue = DateTime.UtcNow;
                    break;
                case { State: EntityState.Modified }:
                    entry.Property(p => p.ChangedAt).CurrentValue = DateTime.UtcNow;
                    break;
            }
        }
        
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}