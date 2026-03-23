namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.CreateCircuitPointCollecte;

public class CreateCircuitPointCollecteResponse
{
    public Ulid CircuitPointCollecteId { get; set; }
    public Ulid CircuitId { get; set; }
    public string CodePointCollecte { get; set; } = string.Empty;
    public string? LibellePointCollecte { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public int? Ordre { get; set; }
}
