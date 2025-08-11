using MediatR;

namespace ItemManagementService.Application.Commands;

public record AddItemTagCommand(Guid ItemId, Guid TagId) : IRequest<bool>;
