using ItemManagementService.Infrastructure.Dtos;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Queries;

public record GetAllItemsQuery() : IRequest<IReadOnlyList<ItemDto>>;
 