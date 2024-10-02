using CourseSelection.Domain.Course;
using CourseSelection.Domain.ReserveUnits;
using CourseSelection.Domain.SeletionTime;
using CourseSelection.Domain.Units;
using Microsoft.EntityFrameworkCore;

namespace CourseSelection.infrastructure.Persistance;


public class CourseSelectionContext : DbContext
{
    public CourseSelectionContext(DbContextOptions<CourseSelectionContext> options) : base(options)
    {
    }

    public DbSet<Courses> Courses { get; set; }
    public DbSet<Units> Units { get; set; }
    public DbSet<SelectionTime> SelectionTimes { get; set; }
    public DbSet<ReserveUnits> ReserveUnits { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Courses.CourseConfiguration());
        modelBuilder.ApplyConfiguration(new Units.UnitsConfiguration());
        modelBuilder.ApplyConfiguration(new ReserveUnits.ReserveUnitsConfiguration());
        modelBuilder.ApplyConfiguration(new SelectionTime.SelectionTimeConfiguration());
    }
}
