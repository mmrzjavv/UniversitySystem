using Identity.Domain.Roles;

namespace Identity.Application.Users.DTO;

public class CreateUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
}