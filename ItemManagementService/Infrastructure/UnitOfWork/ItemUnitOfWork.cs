using Shared.Domain.Item.Repositories;
using Shared.Domain.Location.Repositories;
using Shared.Domain.Tags.Repositories;
using Shared.Persistence;
using Shared.Persistence.Repositories.Common;

namespace ItemManagementService.Infrastructure.UnitOfWork;

public class ItemUnitOfWork(
    AppDbContext context,
    IItemRepository itemRepository) : BaseUnitOfWork(context), IItemUnitOfWork
{
    public IItemRepository Items { get; } = itemRepository;
}
