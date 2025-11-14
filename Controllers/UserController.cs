using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLedger.BAL.Interfaces;
using SmartLedger.BAL.Models.User;

namespace SmartLedger.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // 🔥 GET ALL USERS IN ORG (ADMIN ONLY)
        [HttpGet("organization/{orgId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers(Guid orgId)
        {
            return Ok(await _userService.GetUsersByOrgAsync(orgId));
        }

        // 🔥 GET SINGLE USER
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        // 🔥 UPDATE USER ROLE
        [HttpPut("{id}/role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRole(Guid id, UpdateUserRoleDto dto)
        {
            var result = await _userService.UpdateUserRoleAsync(id, dto.Role);
            if (!result) return NotFound();

            return Ok(new { message = "User role updated successfully." });
        }

        // 🔥 DELETE USER
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();

            return Ok(new { message = "User deleted successfully." });
        }
    }
}
