using Shared.Domain.Item;

namespace ItemManagementService.Infrastructure.Dtos;

public record ItemDto(Guid Id, string Name, Guid TenantId)
{
    public ItemDto(Item item) : this(item.Id, item.Name, item.TenantId)
    {
        
    }
}
