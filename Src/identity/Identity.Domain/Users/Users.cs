using Identity.Domain.Users.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Domain.Users;

public class Users : BaseEntity.BaseEntity
{
    public string FullName { get; private set; }
    public MobileNumber MobileNo { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public Guid RoleId { get; set; }
    public Roles.Roles Roles { get; set; }

    public Users(string fullName, MobileNumber mobileNo, string username, string password, Guid roleId)
    {
        SetFullName(fullName);
        MobileNo = mobileNo;
        SetUsername(username);
        SetPassword(password);
        RoleId = roleId;
    }

    public void SetFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("FullName cannot be empty.");
        FullName = fullName;
    }

    public void SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length > 20)
            throw new ArgumentException("Username is either empty or exceeds the max length.");
        Username = username;
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters long.");
        Password = password;
    }


    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).IsRequired();
            builder.Property(x => x.MobileNo)
                .IsRequired()
                .HasConversion(new MobileNumberConverter()); 
            builder.Property(x => x.Username).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(30);
        }
    }
}
