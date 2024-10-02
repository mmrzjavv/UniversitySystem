using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CourseSelection.Domain.Course;

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

            builder.HasMany(x => x.Units).WithOne(x => x.Course).HasForeignKey(x => x.Id);

        }
    }

}
