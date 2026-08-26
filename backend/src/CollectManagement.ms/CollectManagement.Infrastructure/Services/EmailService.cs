using System.Net;
using System.Net.Mail;
using CollectManagement.Application.Common;
using CollectManagement.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CollectManagement.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public Task SendAdminNotificationAsync(string subject, string body, CancellationToken cancellationToken = default)
    {
        var targetAdminEmail = !string.IsNullOrWhiteSpace(_emailSettings.AdminNotificationEmail)
            ? _emailSettings.AdminNotificationEmail
            : "rayenfarhani9@gmail.com";

        return SendEmailAsync(targetAdminEmail, subject, body, cancellationToken);
    }

    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Preparing email notification for Admin target '{To}' with subject '{Subject}'",
            to, subject);

        try
        {
            if (!string.IsNullOrWhiteSpace(_emailSettings.SenderEmail) &&
                !string.IsNullOrWhiteSpace(_emailSettings.SenderPassword))
            {
                using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
                {
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                    EnableSsl = true
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, "PointagePfe Collect Management"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage, cancellationToken).ConfigureAwait(false);
                _logger.LogInformation("Successfully sent email to '{To}'", to);
            }
            else
            {
                _logger.LogWarning(
                    "SMTP Sender credentials not configured in EmailSettings. Simulated email dispatch to '{To}' with Subject: '{Subject}'. Content: {Body}",
                    to, subject, body);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to '{To}' with subject '{Subject}'", to, subject);
        }
    }
}
