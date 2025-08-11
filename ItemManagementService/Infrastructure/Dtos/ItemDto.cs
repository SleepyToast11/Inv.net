namespace ItemManagementService.Infrastructure.Dtos;

public record ItemDto(Guid Id, string Name, Guid TenantId);
