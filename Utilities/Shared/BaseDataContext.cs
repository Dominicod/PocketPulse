using Microsoft.EntityFrameworkCore;
using Utilities.BaseModels;

namespace Utilities.Shared;

public class BaseDataContext : DbContext, IBaseDataContext
{
    public BaseDataContext()
    {
    }
    
    public BaseDataContext(DbContextOptions options) : base(options)
    {
    }
    
    //? Dynamically update CreatedAt and UpdatedAt properties on save
    //? This is done for reporting purposes
    //?
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: BaseModel, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            ((BaseModel)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            var entity = (BaseModel)entityEntry.Entity;
            
            if (entityEntry.State == EntityState.Added)
                entity.CreatedAt = DateTime.UtcNow;
            else
                Entry(entity).Property(x => x.CreatedAt).IsModified = false;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}