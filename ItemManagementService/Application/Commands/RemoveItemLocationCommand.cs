using MediatR;

namespace ItemManagementService.Application.Commands;

public record RemoveItemLocationCommand(Guid ItemId, Guid LocationId) : IRequest<bool>;
