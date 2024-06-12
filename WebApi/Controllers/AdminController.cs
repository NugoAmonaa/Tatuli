using Final.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var userRole = await _userService.GetUserRole(User.Identity.Name);

            if (!User.IsInRole("Administrator"))
            {
                return Forbid(); // Return 403 Forbidden if user doesn't have the required role
            }
            var users = await _userService.GetUsers();
            return Ok(users);
        }


    }
}
