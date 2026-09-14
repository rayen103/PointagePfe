namespace CollectManagement.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendAdminNotificationAsync(string subject, string body, CancellationToken cancellationToken = default);
    Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
}
