using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record AddItemLocationCommand(Guid ItemId, Guid LocationId, int Quantity) : IRequest<bool>;
