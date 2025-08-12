using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record DeleteItemCommand(Guid ItemId) : IRequest<bool>;
