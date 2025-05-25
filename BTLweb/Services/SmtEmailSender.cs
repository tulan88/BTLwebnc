using System.Net;
using System.Net.Mail;

namespace BTLweb.Services
{
    public class SmtEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SmtEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var username = _configuration["EmailSettings:Username"];
            var password = _configuration["EmailSettings:Password"];
            var displayName = _configuration["EmailSettings:DisplayName"];

            var mail = new MailMessage
            {
                From = new MailAddress(username, displayName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mail.To.Add(toEmail);

            using var smtp = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }
    }

}
