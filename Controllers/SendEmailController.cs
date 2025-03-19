using G5_MovieTicketBookingSystem.Util;
using Microsoft.AspNetCore.Mvc;

namespace G5_MovieTicketBookingSystem.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        private readonly EmailSender _emailSender;

        public EmailController(EmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] EmailRequest request)
        {
            bool result = _emailSender.SendEmail(request.ToEmail, request.Subject, request.Body);

            if (result)
                return Ok(new { message = "Email sent successfully!" });
            else
                return BadRequest(new { message = "Failed to send email." });
        }
    }

    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
