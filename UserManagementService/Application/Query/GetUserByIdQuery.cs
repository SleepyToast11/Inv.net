using MediatR;
using UserManagementService.Infrastructure.Dto;

namespace UserManagementService.Application.Query;

public record GetUserByIdQuery(Guid Id): IRequest<UserApplicationDto?>;