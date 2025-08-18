using MediatR;
using UserManagementService.Infrastructure.Dto;

namespace UserManagementService.Application.Query;

public record GetAllUsersByTenantQuery(Guid TenantId): IRequest<IReadOnlyList<UserApplicationDto>>;