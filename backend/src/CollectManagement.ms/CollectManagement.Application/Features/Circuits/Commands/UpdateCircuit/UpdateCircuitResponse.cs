namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public class UpdateCircuitResponse
{
    public Ulid CircuitId { get; set; }
    public string CodeCircuit { get; set; } = string.Empty;
    public string? LibelleCircuit { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsActive { get; set; }
    public string? CodePCDepart { get; set; }
    public string? CodePCArrivee { get; set; }
    public decimal? DistanceKm { get; set; }
    public int? DureeMinutes { get; set; }
    public string? Couleur { get; set; }
}
