using Azure;
using Azure.Communication.Email;
using boulderlog.Domain;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace boulderlog.Domain.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly AppConfigOptions _appConfig;

        public EmailSender(ILoggerFactory logger, IOptions<AppConfigOptions> appConfig)
        {
            _logger = logger.CreateLogger<EmailSender>();
            _appConfig = appConfig.Value;
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
                await emailClient.SendAsync(WaitUntil.Completed, _appConfig.DoNotReplyEmail, email, $"BoulderLog - {subject}", htmlMessage);
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
