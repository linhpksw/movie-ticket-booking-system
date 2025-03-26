using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace G5_MovieTicketBookingSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userRequestDto)
        {
            var UserDto = await _userServices.Login(userRequestDto);

            if (UserDto == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            List<UserRole> userRoles = await _userServices.GetUserRolesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserDto.Email),
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()?.RoleId.ToString() ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
            return Ok(UserDto);
        }
    }
}
