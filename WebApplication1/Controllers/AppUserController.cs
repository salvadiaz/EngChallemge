using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.User;
using WebApplication1.Interfaces;
using WebApplication1.Mappers;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/users")]
public class AppUserController : ControllerBase
{
    private static string _USER_ROLE = "User";
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AppUserController(IUserService userService, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _userService = userService;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = userDto.toEntity();
            var createdUser = await _userService.RegisterUser(user, userDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userService.AddUserToRole(user, _USER_ROLE);
                if (roleResult.Succeeded)
                {
                    return Created(
                        "api/users",
                        user.toDtoFromCreated()
                    );
                }
                else
                {
                    return BadRequest(roleResult.Errors);
                }
            }
            else
            {
                return BadRequest(createdUser.Errors);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("{userId}")]
    [Authorize]
    public async Task<IActionResult> UpdateActiveStatus([FromRoute] string userId, [FromBody] bool status)
    {
        try
        {
            var updatedUser = await _userService.UpdateActiveStatus(status, userId);
            if (updatedUser == null) return NotFound("User not found");
            
            return Ok(updatedUser.toDto());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{userId}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] string userId)
    {
        try
        {
            var deletedUser = await _userService.Delete(userId);
            if (deletedUser == null) return NotFound("User not found");
            
            return Ok(deletedUser.Id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("active")]
    [Authorize]
    public async Task<IActionResult> GetAllActiveUsers()
    {
        try
        {
            var users = await _userService.GetAllActiveUsers();
            return Ok(users.Select(u => u.toDto()));
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _userService.GetByUsername(loginDto.Username);
        if (user == null) return Unauthorized("Invalid username");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized("Username not found or invalid password");

        var token = _tokenService.CreateToken(user);
        return Ok(user.ToLoggedUserDto(token));
    }
}