using System.Security.Claims;
using AuthService.Dtos;
using Shared.Domain.ApplicationUser;
using Shared.Persistence.Entities.ApplicationUser;

namespace AuthService.Services;

public interface IAuthService
{
    Task<(bool Success, string[] Errors)> RegisterUserAsync(RegisterDto dto,
        CancellationToken cancellationToken = default);

    Task<ApplicationUserEntity?> ValidateUserAsync(LoginDto dto,
        CancellationToken cancellationToken = default);

    Task<IList<Claim>> GenerateClaimsAsync(ApplicationUser user,
        CancellationToken cancellationToken = default);
}