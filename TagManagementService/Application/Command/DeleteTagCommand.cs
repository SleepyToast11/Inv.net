using MediatR;

namespace TagManagementService.Application.Command;

public record DeleteTagCommand(Guid id): IRequest;