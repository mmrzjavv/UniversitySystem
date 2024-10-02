using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitSelection.Domain.courses;
[Table("Courses",Schema = "UnitsSelection")]
public class Courses : BaseEntity.BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int UnitsCount { get; set; }

    public ICollection<Units.Units> Units { get; set; } = [];

    public class CourseConfiguration : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.UnitsCount).IsRequired();

            builder.HasMany(x => x.Units).WithOne(x => x.Course).HasForeignKey(x => x.CourseId);
        }
    }

}
