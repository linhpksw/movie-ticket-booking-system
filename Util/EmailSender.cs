using System.Net;
using System.Net.Mail;

namespace G5_MovieTicketBookingSystem.Util
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");

                string smtpServer = emailSettings["SMTPServer"];
                int port = int.Parse(emailSettings["Port"]);
                bool enableSSL = bool.Parse(emailSettings["EnableSSL"]);
                string username = emailSettings["Username"];
                string password = emailSettings["Password"];
                string fromEmail = emailSettings["FromEmail"];

                using (var smtp = new SmtpClient(smtpServer, port))
                {
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = enableSSL;

                    using (var message = new MailMessage(fromEmail, toAddress, subject, body))
                    {
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                    }
                }
                Console.WriteLine("✅ Email sent successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send email. Error: {ex.Message}");
                return false;
            }
        }
    }
}
