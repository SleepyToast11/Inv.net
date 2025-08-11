using MediatR;

namespace ItemManagementService.Application.Commands;

public record UpdateItemLocationQuantityCommand(Guid ItemId, Guid LocationId, int NewQuantity) : IRequest<bool>;
