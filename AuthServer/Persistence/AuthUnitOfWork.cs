using Shared.Domain.ApplicationUser.Repositories;
using Shared.Domain.Tenant.Repositories;
using Shared.Persistence;

namespace AuthService.Persistence;

public class AuthUnitOfWork(
    AppDbContext context,
    IApplicationUserRepository applicationUserRepository,
    ITenantRepository tenantRepository) : IAuthUnitOfWork
{
    private bool _disposed;
    public IApplicationUserRepository ApplicationUsers { get; } = applicationUserRepository;
    public ITenantRepository Tenants { get; } = tenantRepository;

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
            if (disposing) context.Dispose();
            _disposed = true;
        }
    }
}