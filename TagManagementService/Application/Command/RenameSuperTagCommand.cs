using MediatR;

namespace TagManagementService.Application.Command;

public record RenameSuperTagCommand(Guid id, string NewName):  IRequest<bool>;