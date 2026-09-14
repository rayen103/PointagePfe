namespace CollectManagement.Application.Common;

public class EmailSettings
{
    public const string SectionName = "EmailSettings";

    public string AdminNotificationEmail { get; set; } = "rayenfarhani9@gmail.com";
    public string SmtpServer { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string? SenderEmail { get; set; }
    public string? SenderPassword { get; set; }
}
