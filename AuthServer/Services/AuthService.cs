using System.Security.Claims;
using System.Text.Json;
using AuthService.Dtos;
using AuthService.Persistence;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using Shared.Domain.ApplicationUser;
using Shared.Dtos;
using Shared.Persistence.Entities.ApplicationUser;

namespace AuthService.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IAuthUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUserEntity> _userManager;

    public AuthService(IAuthUnitOfWork unitOfWork, UserManager<ApplicationUserEntity> userManager,
        ILogger<AuthService> logger)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<(bool Success, string[] Errors)> RegisterUserAsync(RegisterDto dto,
        CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUserEntity
        {
            Id = Guid.NewGuid(),
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        return result.Succeeded
            ? (true, Array.Empty<string>())
            : (false, result.Errors.Select(e => e.Description).ToArray());
    }

    public async Task<ApplicationUserEntity?> ValidateUserAsync(LoginDto dto,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            user = new ApplicationUserEntity();
            await _userManager.CheckPasswordAsync(user, "");
            return null;
        }

        var valid = await _userManager.CheckPasswordAsync(user, dto.Password);
        return valid ? user : null;
    }

    public async Task<IList<Claim>> GenerateClaimsAsync(ApplicationUser user,
        CancellationToken cancellationToken = default)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                .SetDestinations(OpenIddictConstants.Destinations.AccessToken,
                    OpenIddictConstants.Destinations.IdentityToken),

            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
                .SetDestinations(OpenIddictConstants.Destinations.AccessToken,
                    OpenIddictConstants.Destinations.IdentityToken),

            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
                .SetDestinations(OpenIddictConstants.Destinations.AccessToken,
                    OpenIddictConstants.Destinations.IdentityToken)
        };

        var userWithPermissions = await _unitOfWork.ApplicationUsers
            .GetByIdAsync(user.Id, cancellationToken, true);

        if (userWithPermissions?.UserPermissionEntities != null && userWithPermissions.UserPermissionEntities.Any())
        {
            var tenantPermissions = user.TenantPermissions
                .GroupBy(p => p.TenantId)
                .Select(g => new TenantPermissionClaim
                {
                    TenantId = g.Key,
                    Permissions = g
                        .SelectMany(p => p.Permissions)
                        .GroupBy(p => p.Key)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Max(x => (int)x.Value).ToString() // Convert highest level to string
                        )
                })
                .ToList();

            var json = JsonSerializer.Serialize(tenantPermissions);
            var permissionsClaim = new Claim("tenant_permissions", json)
                .SetDestinations(OpenIddictConstants.Destinations.AccessToken);

            claims.Add(new Claim("tenant_permissions", json));
        }

        return claims;
    }
}