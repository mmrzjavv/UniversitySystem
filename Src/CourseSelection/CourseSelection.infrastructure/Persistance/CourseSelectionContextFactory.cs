using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CourseSelection.infrastructure.Persistance
{
    public class CourseSelectionContextFactory : IDesignTimeDbContextFactory<CourseSelectionContext>
    {
        public CourseSelectionContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CourseSelectionContext>();

            // بارگذاری تنظیمات از appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // گرفتن ConnectionString
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new CourseSelectionContext(optionsBuilder.Options);
        }
    }
}
