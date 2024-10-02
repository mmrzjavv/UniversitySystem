using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UnitSelection.Domain.courses;

namespace UnitSelection.Domain.Units;

[Table("Units" , Schema = "UnitsSelection")]
public class Units : BaseEntity.BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Teacher { get; set; } = string.Empty;
    public int AllCapacity { get; set; }
    public int ReservedCapacity { get; set; }
    public DateTime ExamTime { get; set; }
    public decimal Price { get; set; }

    public Courses Course { get; set; } = null!;
    public Guid CourseId { get; set; }

    public ICollection<ReserveUnits.ReserveUnits> ReserveUnits { get; set; } = [];

    public class UnitsConfiguration : IEntityTypeConfiguration<Units>
    {
        public void Configure(EntityTypeBuilder<Units> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(400);
            builder.Property(x => x.Teacher).IsRequired().HasMaxLength(700);
            builder.Property(x => x.AllCapacity).IsRequired();
            builder.Property(x => x.ReservedCapacity).IsRequired();
            builder.Property(x => x.ExamTime).IsRequired();
            builder.Property(X => X.Price).IsRequired();

            builder.HasMany(x => x.ReserveUnits).WithOne(x => x.Units).HasForeignKey(x => x.UnitId);

        }
    }
}
