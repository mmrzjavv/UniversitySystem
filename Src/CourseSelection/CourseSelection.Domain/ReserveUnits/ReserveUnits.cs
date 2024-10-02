using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSelection.Domain.ReserveUnits
{
    public class ReserveUnits : BaseEntity.BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid UnitId { get; set; }
        public Units.Units Units { get; set; } = null!;

        public class ReserveUnitsConfiguration : IEntityTypeConfiguration<ReserveUnits>
        {
            public void Configure(EntityTypeBuilder<ReserveUnits> builder)
            {
                builder.HasKey(x => x.Id);
            }
        }
    }
}
