namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.UpdateCircuitPointCollecte;

public class UpdateCircuitPointCollecteResponse
{
    public Ulid CircuitPointCollecteId { get; set; }
    public Ulid CircuitId { get; set; }
    public string CodePointCollecte { get; set; } = string.Empty;
    public string? LibellePointCollecte { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public int? Ordre { get; set; }
}
