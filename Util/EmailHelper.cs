using System.Net.Mail;
using System.Net;

namespace G5_MovieTicketBookingSystem.Util
{
    public class EmailHelper
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("syhaoc2dh@gmail.com", "Event System");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "jvayeueatgpvysaw";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
    }
}
