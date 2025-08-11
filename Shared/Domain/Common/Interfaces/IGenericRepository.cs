using System.Linq.Expressions;
using Shared.Persistence.Entities.Common.Interfaces;
using Shared.Persistence.Repositories.Common;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Domain.Common.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool deepLoad = true,
        Expression<Func<T, bool>>? whereFilter = null);

    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken, bool deepLoad,
        Expression<Func<T, bool>>? whereFilter = null);

    Task<IPagedResult<T>> GetPagedAsync(PageRequest pageRequest, CancellationToken cancellationToken, bool deepLoad,
        Expression<Func<T, bool>>? whereFilter = null);

    Task AddAsync(T t, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, Action<T> updateAction, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}