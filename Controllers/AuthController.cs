using G5_MovieTicketBookingSystem.DTOs.UserDto;
using G5_MovieTicketBookingSystem.Services;
using Microsoft.AspNetCore.Authentication;
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
        [HttpGet("login-google-info")]
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync("Google");
            if (result?.Principal == null)
            {
                return Redirect("/sign-up?error=GoogleAuthFailed");
            }

            // Extract user info from claims
            var userInfo = new GoogleLoginUserDto
            {
                Email = result.Principal.FindFirst("email")?.Value,
                FirstName = result.Principal.FindFirst("given_name")?.Value,
                LastName = result.Principal.FindFirst("family_name")?.Value,
                FullName = result.Principal.FindFirst("name")?.Value,
                GoogleId = result.Principal.FindFirst("sub")?.Value, // Google's unique user ID
                Picture = result.Principal.FindFirst("picture")?.Value // Profile picture URL
            };

            if (string.IsNullOrEmpty(userInfo.Email))
            {
                return Redirect("/sign-up?error=EmailNotFound");
            }

            // Map Google user info to your UserCreateDto for registration
            var userDto = new UserCreateDto
            {
                Email = userInfo.Email,
                Password = "GoogleAuth_" + Guid.NewGuid().ToString(), // Random password for Google users
                PasswordConfirm = "GoogleAuth_" + Guid.NewGuid().ToString(),
                fullname = userInfo.FullName ?? $"{userInfo.FirstName} {userInfo.LastName}"
            };

            // Register the user using your service
            var userResponse = await _userServices.Register(userDto, new List<int> { 10 }); // Role ID 10 as per your logic
            if (userResponse == null)
            {
                return Redirect("/sign-up?error=AccountExists");
            }

            // Sign the user in using cookie authentication
            await HttpContext.SignInAsync(result.Principal);

            return Redirect("/"); // Redirect to home page after successful login
        }
    }
}
