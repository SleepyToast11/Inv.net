using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record AddItemTagCommand(Guid ItemId, Guid TagId) : IRequest<bool>;
