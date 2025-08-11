using MediatR;

namespace ItemManagementService.Application.Commands;

public record AddItemLocationCommand(Guid ItemId, Guid LocationId, int Quantity) : IRequest<bool>;
