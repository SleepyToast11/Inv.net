using Microsoft.EntityFrameworkCore;
using Shared.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        var env = context.HostingEnvironment;
    })
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("Postgres");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'Postgres' not found in configuration.");
        }

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
    })
    .Build();

using var scope = host.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

Console.WriteLine("Applying migrations...");
await dbContext.Database.MigrateAsync();
Console.WriteLine("Database migration complete.");
