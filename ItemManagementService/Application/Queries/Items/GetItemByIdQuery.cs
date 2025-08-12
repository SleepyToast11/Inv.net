using ItemManagementService.Infrastructure.Dtos;
using MediatR;

namespace ItemManagementService.Application.Queries.Items;

public record GetItemByIdQuery(Guid ItemId) : IRequest<ItemDto?>;
