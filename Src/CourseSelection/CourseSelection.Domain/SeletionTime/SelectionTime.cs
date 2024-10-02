using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelection.Domain.SeletionTime
{
    public class SelectionTime : BaseEntity.BaseEntity
    {
        public string TimeName { get; set; } = string.Empty;

        public ICollection<Units.Units> Units { get; set; } = [];

        public class SelectionTimeConfiguration : IEntityTypeConfiguration<SelectionTime>
        {
            public void Configure(EntityTypeBuilder<SelectionTime> builder)
            {
               builder.HasKey(x => x.Id);
                builder.Property(x => x.TimeName).IsRequired().HasMaxLength(50);

                builder.HasMany(x => x.Units).WithOne(x => x.SelectionTime).HasForeignKey(x => x.SelectionTimeId);
            }
        }
    }
}
