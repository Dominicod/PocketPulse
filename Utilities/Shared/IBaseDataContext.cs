namespace Utilities.Shared;

public interface IBaseDataContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}