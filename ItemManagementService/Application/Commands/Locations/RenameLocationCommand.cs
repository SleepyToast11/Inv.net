using MediatR;

namespace ItemManagementService.Application.Commands.Locations;

public record RenameLocationCommand(Guid Id, string NewName):  IRequest<bool>;