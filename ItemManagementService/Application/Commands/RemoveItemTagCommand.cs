using MediatR;

namespace ItemManagementService.Application.Commands;

public record RemoveItemTagCommand(Guid ItemId, Guid TagId) : IRequest<bool>;
