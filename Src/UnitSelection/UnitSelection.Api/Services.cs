using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using UnitSelection.infrastructure.Persistance;

namespace UnitSelection.Api
{
    public static class Services
    {
        public static IServiceCollection AddDbConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<UnitSelectionsContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            return builder.Services;
        }
    }
}