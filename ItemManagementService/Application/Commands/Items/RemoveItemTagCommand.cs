using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record RemoveItemTagCommand(Guid ItemId, Guid TagId) : IRequest<bool>;
