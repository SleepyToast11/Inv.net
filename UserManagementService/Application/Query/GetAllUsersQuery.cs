using MediatR;
using UserManagementService.Infrastructure.Dto;

namespace UserManagementService.Application.Query;

public record GetAllUsersQuery(): IRequest<IReadOnlyList<UserApplicationDto>>;