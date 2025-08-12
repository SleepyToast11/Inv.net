using ItemManagementService.Infrastructure.Dtos;
using MediatR;

namespace ItemManagementService.Application.Queries.Items;

public record GetAllItemsQuery() : IRequest<IReadOnlyList<ItemDto>>;
 