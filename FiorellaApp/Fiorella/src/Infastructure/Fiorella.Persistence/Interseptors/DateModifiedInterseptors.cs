using Fiorella.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Fiorella.Persistence.Interseptors;
public class DateModifiedInterseptors:SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
          DbContextEventData eventData,
          InterceptionResult<int> result,
          CancellationToken cancellationToken = default)
    {
        UpdateDateProperties(eventData.Context.ChangeTracker.Entries());
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private static void UpdateDateProperties(IEnumerable<EntityEntry> entries)
    {
        var now = DateTime.Now;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added && entry.Entity is BaseEntity)
            {
                var entity = (BaseEntity)entry.Entity;
                entity.DateCreated = now;
                entity.DateModified = now;
            }
            else if (entry.State == EntityState.Modified && entry.Entity is BaseEntity)
            {
                var entity = (BaseEntity)entry.Entity;
                entity.DateModified = now;
            }
        }
    }
}
