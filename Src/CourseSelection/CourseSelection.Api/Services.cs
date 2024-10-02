using Asp.Versioning;
using CourseSelection.Application.Course;
using CourseSelection.Domain;
using CourseSelection.infrastructure.Persistance;
using CourseSelection.infrastructure.Service;
using Microsoft.EntityFrameworkCore;

namespace CourseSelection.Api;

public static class Services
{
    public static IServiceCollection AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICourse, CourseManager>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
        builder.Services.AddAutoMapper(typeof(CourseMapping));
        return builder.Services;
    }

    public static IServiceCollection AddDbConfig(this WebApplicationBuilder builder)
    {
        // Add DbContext with logging enabled
        builder.Services.AddDbContext<CourseSelectionContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                    .LogTo(Console.WriteLine, LogLevel.Information) // Enable logging to console
                    .EnableSensitiveDataLogging() // Enable sensitive data logging for detailed logs (optional)
        );
        return builder.Services;
    }

    public static IServiceCollection AddVersioningConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
            .AddMvc() 
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
        return builder.Services;
    }
}