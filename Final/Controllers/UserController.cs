using Final.Dto;
using Final.Entities;
using Final.Interfaces;
using Final.IRepositories;
using Final.Services;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await _userService.GetUsers();

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
             await _userService.AddUser(addUserDto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            await _userService.UpdateUser(updateUserDto);
            return Ok();
        }

        [HttpGet("{email}")] 
        public async Task<IActionResult> GetUser(string email)
        {

            return Ok(await _userService.GetUser(email));

        }
    }

}
