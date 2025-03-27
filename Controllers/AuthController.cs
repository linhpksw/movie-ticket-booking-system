using G5_MovieTicketBookingSystem.Commons;
using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Models;
using G5_MovieTicketBookingSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace G5_MovieTicketBookingSystem.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;
        [CascadingParameter]
        public HttpContext? HttpContext { get; set; }

        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            UserResponseDto response = await _userServices.Login(request);

            if (response == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            int roleId = CommonConstant.CUSTOMER_ROLE;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, response.Email),
                new Claim(ClaimTypes.Role, roleId.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            if (HttpContext != null)
            {
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal
                );
                Console.WriteLine("haha");
            }
            return Ok(response);
        }
    }
}
