using Identity.Application.Roles;
using Identity.Application.Users;
using Identity.Domain.Roles.Factory;
using Identity.Domain.Users.Factory;
using Identity.Domain.Users.ValueObject;
using Identity.infrastructure.Persistance;
using Identity.infrastructure.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext (configure your database connection here)
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add generic repository and application services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<RoleApplicationService>();
builder.Services.AddScoped<UserApplicationService>();

builder.Services.AddScoped<UserFactory>();
builder.Services.AddScoped<RoleFactory>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add the exception handling middleware
app.UseMiddleware<Identity.Api.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); // Enable controller mapping

app.Run();
