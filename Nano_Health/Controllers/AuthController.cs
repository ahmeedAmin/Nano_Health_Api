using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Nano_Health.Services.Interfaces;
using Nano_Health.Services.Repositories;
using Nano_Health.Models;
using Nano_Health.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Nano_Health.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new { Message = "Email is already exist" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var user = new User()
            {        
                FName = model.FName,
                LName = model.LName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
            };

           var result = await _userManager.CreateAsync(user, model.Password);

            
            if (!result.Succeeded)
                return BadRequest(new { Message = "Something went wrong while registering!", Errors = result.Errors });

          
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole("User"));
                if (!roleResult.Succeeded)
                    return StatusCode(500, new { Message = "Failed to create role" });
            }

            // Add user to role
            var roleAssignResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleAssignResult.Succeeded)
                return BadRequest(new { Message = "Failed to assign user to role", Errors = roleAssignResult.Errors });

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return Unauthorized(new { Message = "Email is not exist" });

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { Message = "Password is not Valid!!" });

            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(new UserDto()
            {
                Role = string.Join(",", userRoles),
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
                Message = "success"
            });
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
      
}
