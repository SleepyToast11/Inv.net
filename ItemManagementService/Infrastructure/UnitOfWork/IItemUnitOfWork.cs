using Shared.Domain.Item.Repositories;
using Shared.Domain.Location.Repositories;
using Shared.Domain.Tags.Repositories;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace ItemManagementService.Infrastructure.UnitOfWork;

public interface IItemUnitOfWork: IDisposable, IUnitOfWork
{
    IItemRepository Items { get; }
    ILocationRepository Locations { get; }
}