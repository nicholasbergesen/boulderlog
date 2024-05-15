using Azure;
using Azure.Communication.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace boulderlog.Domain.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<EmailSender>();
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string? connectionString = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogInformation("COMMUNICATION_SERVICES_CONNECTION_STRING environment variable not found, no email sent.");
                return;
            }

            try
            {
                var emailClient = new EmailClient(connectionString);
                await emailClient.SendAsync(WaitUntil.Completed, "DoNotReply@49b5638d-ff62-42b4-8e2e-ed6e33923ade.azurecomm.net", email, $"Boulderlog - {subject}", htmlMessage);
                _logger.LogInformation("Sent {subject} email to {email}", subject, email);
            }
            catch (SmtpException e)
            {
                _logger.LogError(e, "Failed to send {subject} to {email}", subject, email);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to send emails.");
            }
        }
    }
}
