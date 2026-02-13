namespace CollectManagement.Application.Features.PointsCollecte.Commands.UpdatePointCollecte;

public class UpdatePointCollecteResponse
{
    public Ulid PointCollecteId { get; set; }
    public string CodePointCollecte { get; set; } = string.Empty;
    public string LibellePointCollecte { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? CodeGouvernorat { get; set; }
    public string? CodeRegion { get; set; }
    public bool IsActive { get; set; }
}
