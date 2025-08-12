using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record RemoveItemLocationCommand(Guid ItemId, Guid LocationId) : IRequest<bool>;
