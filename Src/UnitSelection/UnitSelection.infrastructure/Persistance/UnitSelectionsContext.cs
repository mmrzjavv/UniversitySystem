using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitSelection.Domain.courses;
using UnitSelection.Domain.ReserveUnits;
using UnitSelection.Domain.Units;

namespace UnitSelection.infrastructure.Persistance
{
    public class UnitSelectionsContext : DbContext
    {
        public UnitSelectionsContext()
        {
        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<ReserveUnits> ReserveUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=.;Database=UnitSelections;Username=postgres;Password=Moh8357344");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Courses.CourseConfiguration());
            modelBuilder.ApplyConfiguration(new Units.UnitsConfiguration());
            modelBuilder.ApplyConfiguration(new ReserveUnits.ReserveUnitsConfiguration());
        }
    }
}