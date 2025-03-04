using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace G5_MovieTicketBookingSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : Controller
    {
        private readonly IUserServices _userServices;

        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Invalid login request");
            }

            Task<UserResponseDto> UserDto = _userServices.LoginAsync(loginRequest);

            return null;
            // Xác thực người dùng (thay bằng logic thực tế: kiểm tra database)
          
        }
    }
}
