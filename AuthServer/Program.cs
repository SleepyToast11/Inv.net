using AuthService.handlers;
using AuthService.Persistence;
using AuthService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Server;
using Shared.Domain.ApplicationUser.Repositories;
using Shared.Domain.Tenant.Repositories;
using Shared.Persistence;
using Shared.Persistence.Entities.ApplicationUser;
using Shared.Persistence.Repositories.UserRepositories;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// DB & Identity Setup
// ------------------------

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

    // Required for OpenIddict to work with EF Core stores
    options.UseOpenIddict();
});

builder.Services.AddIdentity<ApplicationUserEntity, IdentityRole<Guid>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ------------------------
// OpenIddict Setup
// ------------------------

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
            .UseDbContext<AppDbContext>();
    })
    .AddServer(options =>
    {
        options.SetTokenEndpointUris("/connect/token");

        options.AllowPasswordFlow(); // Enable 'password' grant
        options.AcceptAnonymousClients(); // No client_id required

        options.AddDevelopmentEncryptionCertificate()
            .AddDevelopmentSigningCertificate();

        options.UseAspNetCore()
            .EnableTokenEndpointPassthrough();

        options.DisableAccessTokenEncryption(); // Optional: leave token readable

        // Register event handler
        options.AddEventHandler<OpenIddictServerEvents.HandleTokenRequestContext>(builder =>
            builder.UseScopedHandler<PasswordGrantHandler>());
    })
    .AddValidation(options =>
    {
        options.UseLocalServer();
        options.UseAspNetCore();
    });


builder.Services.AddScoped<IAuthService, AuthService.Services.AuthService>();
builder.Services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();