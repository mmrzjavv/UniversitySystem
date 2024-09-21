using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Application.Roles;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RolesController : ControllerBase
    {
        private readonly RoleApplicationService _roleApplicationService;

        public RolesController(RoleApplicationService roleApplicationService)
        {
            _roleApplicationService = roleApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            await _roleApplicationService.CreateRoleAsync(request.DisplayName, request.Description);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var roles = await _roleApplicationService.GetAllRole();
            return Ok(roles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(Guid id)
        {
            await _roleApplicationService.DeleteRoleAsync(id);
            return NoContent();
        }
    }

    public class CreateRoleRequest
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}