using MediatR;

namespace ItemManagementService.Application.Commands;

public record UpdateItemNameCommand(Guid ItemId, string NewName) : IRequest<bool>;
