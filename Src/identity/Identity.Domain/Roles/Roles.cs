using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Domain.Roles
{
    public class Roles : BaseEntity.BaseEntity
    {
        public string DisplayName { get; private set; }
        public string Description { get; private set; }

        public ICollection<Users.Users> Users { get;  set; } = [];

        public Roles(string displayName, string description)
        {
            SetDisplayName(displayName);
            SetDescription(description);
        }

        private void SetDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("DisplayName cannot be empty.");
            DisplayName = displayName;
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");
            Description = description;
        }

        public class RoleConfiguration : IEntityTypeConfiguration<Roles>
        {
            public void Configure(EntityTypeBuilder<Roles> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(200);
                builder.Property(x => x.Description).IsRequired().HasMaxLength(700);

                builder.HasMany(x => x.Users).WithOne(x => x.Roles).HasForeignKey(x => x.RoleId);
            }
        }
    }
}