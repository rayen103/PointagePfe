namespace CollectManagement.Application.Features.Reseaux.Queries.GetOneReseau;

public class GetOneReseauDto
{
    public Ulid ReseauId { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public int Port { get; set; }
    public int? GmtPlus { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Rayon { get; set; }
    public int? TimeToleranceMinute { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
