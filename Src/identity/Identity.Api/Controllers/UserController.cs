using Identity.Application;
using Identity.Application.Users;
using Identity.Application.Users.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api;

[ApiController]
[Route("api/v1/Users")]
public class UserController(UserApplicationService userApplicationService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        await userApplicationService.CreateUserAsync(request.FullName, request.MobileNo, request.Username,
            request.Password, request.RoleId);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await userApplicationService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await userApplicationService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
    {
        var user = await userApplicationService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        user.SetFullName(request.FullName);
        user.SetPassword(request.Password);
        await userApplicationService.UpdateUserAsync(user);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await userApplicationService.DeleteUserAsync(id);
        return Ok();
    }
}