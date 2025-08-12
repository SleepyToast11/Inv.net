using MediatR;

namespace TagManagementService.Application.Command;

public record DeleteSuperTagCommand(Guid Id): IRequest<bool>;