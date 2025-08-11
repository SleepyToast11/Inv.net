using Shared.Domain.ApplicationUser.Repositories;
using Shared.Domain.Tenant.Repositories;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace AuthService.Persistence;

public interface IAuthUnitOfWork : IDisposable, IUnitOfWork
{
    IApplicationUserRepository ApplicationUsers { get; }
    ITenantRepository Tenants { get; }
}