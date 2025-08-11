using MediatR;

namespace ItemManagementService.Application.Commands;

public record DeleteItemCommand(Guid ItemId) : IRequest<bool>;
