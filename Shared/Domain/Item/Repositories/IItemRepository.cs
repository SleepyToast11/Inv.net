using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.Item;

namespace Shared.Domain.Item.Repositories;

public interface IItemRepository : IGenericRepository<ItemEntity>
{
}