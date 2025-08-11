using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Persistence.Repositories.Common;

public abstract class BaseUnitOfWork(AppDbContext context) : IUnitOfWork
{
    private bool _disposed;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
                context.Dispose();

            _disposed = true;
        }
    }
}