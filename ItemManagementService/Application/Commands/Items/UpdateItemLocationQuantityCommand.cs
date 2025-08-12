using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record UpdateItemLocationQuantityCommand(Guid ItemId, Guid LocationId, int NewQuantity) : IRequest<bool>;
