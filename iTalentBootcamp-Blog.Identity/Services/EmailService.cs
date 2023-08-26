using iTalentBootcamp_Blog.Identity.Configurations;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace iTalentBootcamp_Blog.Identity.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigurations _emailConfig;

        public EmailService(IOptions<EmailConfigurations> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }

        public async Task SendResetPasswordEmailAsync(string resetLink, string email)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _emailConfig.Host;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(_emailConfig.Email, _emailConfig.Key);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailConfig.Email);
            mailMessage.To.Add(email);
            mailMessage.Subject = "Localhost | Şifre sıfırlama linki";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = @$"<h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h4>
                                <p>
                                    <a href='{resetLink}'>{resetLink}</a>
                                </p>";

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
