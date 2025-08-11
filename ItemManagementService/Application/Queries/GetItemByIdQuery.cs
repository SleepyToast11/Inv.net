using ItemManagementService.Infrastructure.Dtos;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Queries;

public record GetItemByIdQuery(Guid ItemId) : IRequest<ItemDto?>;
