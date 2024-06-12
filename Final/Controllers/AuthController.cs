﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Final.Dto;
using Final.Entities;
using Final.Enum;
using Final.Interfaces;
using Final.IRepositories;
using Final.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserService _userService;
        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {

            var user = await _userService.GetUser(loginRequest.Email, loginRequest.Password);

            if (user == null || user.IsBanned)
            {
                return Unauthorized();
            }

            bool isAdmin = user.Role == EUserRole.Administrator;

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
    };

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120), // Token expiration time
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenString);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AddUserDto addUserDto)
        {
            // Add user to the database
            await _userService.AddUser(addUserDto);

            // Check if the user being registered is an administrator
            bool isAdmin = addUserDto.Role == EUserRole.Administrator; // Assuming Role property is present in AddUserDto

            // Add "Administrator" role claim if the user is an administrator
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, addUserDto.Email), // Assuming Email is used as the username
        // Add other claims as needed
    };

            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            // Generate JWT token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenString);
        }

    }
}

