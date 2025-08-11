using invProj.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.ApplicationUser;
using Shared.Persistence;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var services = new ServiceCollection();

builder.Services.AddSingleton<Config>();

// Configure DB context based on environment
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var env = builder.Environment;

    if (env.IsDevelopment())
    {
        var dbPath = Path.Combine(env.ContentRootPath, "dev.db");
        options.UseSqlite($"Data Source={dbPath}");
    }
    else
    {
        // Example: "Host=myhost;Database=mydb;Username=myuser;Password=mypass"
        var connString = builder.Configuration.GetConnectionString("Postgres");

        if (string.IsNullOrEmpty(connString))
            throw new InvalidOperationException("PostgreSQL connection string not found.");

        options.UseNpgsql(connString);
    }
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var config = app.Services.GetRequiredService<Config>();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(config.Clients)
    .AddInMemoryApiScopes(config.ApiScopes)
    .AddInMemoryIdentityResources(config.IdentityResources);

app.Run();