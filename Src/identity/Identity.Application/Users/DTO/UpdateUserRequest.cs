namespace Identity.Application.Users.DTO;

public class UpdateUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}