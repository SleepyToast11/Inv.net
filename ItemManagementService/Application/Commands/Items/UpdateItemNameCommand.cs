using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record UpdateItemNameCommand(Guid ItemId, string NewName) : IRequest<bool>;
