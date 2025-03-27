using G5_MovieTicketBookingSystem.DTOs;
using G5_MovieTicketBookingSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace G5_MovieTicketBookingSystem.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            Console.WriteLine("Login");
            var valid = await _userServices.Login(request);
            if (valid == null)
                return Unauthorized("Sai tài khoản hoặc mật khẩu");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Email)
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return Ok("Đăng nhập thành công");
        }
    }
}