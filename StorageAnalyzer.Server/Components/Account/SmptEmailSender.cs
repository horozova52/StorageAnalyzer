using Microsoft.AspNetCore.Identity;
using StorageAnalyzer.Core.Entities;
using System.Net.Mail;
using System.Net;

namespace StorageAnalyzer.Server.Components.Account
{

    public class SmtpEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IConfiguration configuration, ILogger<SmtpEmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            var subject = "Confirm your email";
            var body = $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            var subject = "Reset your password";
            var body = $"Please reset your password by <a href='{resetLink}'>clicking here</a>.";

            await SendEmailAsync(email, subject, body);
        }

        public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            var subject = "Reset your password";
            var body = $"Please reset your password using the following code: {resetCode}";

            await SendEmailAsync(email, subject, body);
        }

        // Metodă "internă" care face trimiterea efectivă a e-mailului
        private async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            // Citești datele din configurație (appsettings.json)
            var smtpSection = _configuration.GetSection("SmtpSettings");
            var smtpServer = smtpSection["Server"];
            var smtpPort = int.Parse(smtpSection["Port"] ?? "587");
            var smtpUser = smtpSection["Username"];
            var smtpPass = smtpSection["Password"];
            var senderName = smtpSection["SenderName"];

            using var mailMessage = new MailMessage()
            {
                From = new MailAddress(smtpUser, senderName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            using var smtpClient = new SmtpClient(smtpServer, smtpPort);
            smtpClient.UseDefaultCredentials = false; // Specifici că nu folosești credențialele implicite ale sistemului
            smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);
            smtpClient.EnableSsl = true; // sau false, în funcție de cerință
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email trimis către {0} cu subiectul {1}", toEmail, subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la trimiterea e-mailului către {0}", toEmail);
                throw;
            }

        }
    }

}
