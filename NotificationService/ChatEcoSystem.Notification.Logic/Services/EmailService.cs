using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    internal class EmailService : IEmailService
    {
        #region CTOR
        /// <inheritdoc cref="SmtpConfiguration"/>
        private readonly SmtpConfiguration _smtpConfiguration;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SmtpConfiguration> options, ILogger<EmailService> logger)
        {
            _logger = logger;
            _smtpConfiguration = options.Value;
        }
        #endregion

        public async Task SendNotificationAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpConfiguration.DisplayName, _smtpConfiguration.From));

                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                using var client = new SmtpClient();

                await client.ConnectAsync(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.StartTlsWhenAvailable);

                await client.AuthenticateAsync(_smtpConfiguration.Username,_smtpConfiguration.Password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Email sent to {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email to {email}");
                throw;
            }
        }
    }
}
