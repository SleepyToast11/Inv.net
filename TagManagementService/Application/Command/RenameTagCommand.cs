using MediatR;

namespace TagManagementService.Application.Command;

public record RenameTagCommand(Guid TagId, string NewName) : IRequest<bool>;
