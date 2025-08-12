using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Shared.Api;
using Shared.Domain.Item.Repositories;
using Shared.Domain.Tags.Repositories;
using Shared.Persistence;
using Shared.Persistence.Repositories.MultiTenancy;
using Shared.Security;
using TagManagementService.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext, you already have your AppDbContext configured elsewhere
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register CurrentTenantAccess and your middleware
builder.Services.AddScoped<CurrentTenantAccess>();
builder.Services.AddScoped<ICurrentTenantAccess>(sp => sp.GetRequiredService<CurrentTenantAccess>());

// Register UnitOfWork and repositories
builder.Services.AddScoped<ITagRepository, EfMtTagRepository>();
builder.Services.AddScoped<ISuperTagRepository, EfMtSuperTagRepository>();
builder.Services.AddScoped<ITagUnitOfWork, TagUnitOfWork>();

// Add MediatR and scan for handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

// Add controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TenantAccessMiddleware>();

app.MapControllers();

app.Run();
