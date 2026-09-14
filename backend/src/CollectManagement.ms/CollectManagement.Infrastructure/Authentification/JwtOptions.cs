namespace CollectManagement.Infrastructure.Authentification;

public class JwtOptions
{
    public static string SectionName { get; } = "JwtOptions";
    public string Secret { get; init; } = "01HBBRZ5CY308W01M2FQVXB0Z5@yelzem-May3ref-3lih-7ad";
    public int ExpiryMinutes { get; init; } = 99999;
    public string Issuer { get; init; } = "AJ.CST";
    public string Audience { get; init; } = "Dispatching";
}