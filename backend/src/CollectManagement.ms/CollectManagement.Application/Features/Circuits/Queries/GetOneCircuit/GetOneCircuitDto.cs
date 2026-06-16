using CollectManagement.Application.Features.CircuitsPointsCollecte.Queries.GetByCircuit;

namespace CollectManagement.Application.Features.Circuits.Queries.GetOneCircuit;

public class GetOneCircuitDto
{
    public Ulid CircuitId { get; set; }
    public string CodeCircuit { get; set; } = string.Empty;
    public string? LibelleCircuit { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
    public string? CodePCDepart { get; set; }
    public string? CodePCArrivee { get; set; }
    public decimal? DistanceKm { get; set; }
    public int? DureeMinutes { get; set; }
    public string? Couleur { get; set; }
    public IReadOnlyList<GetByCircuitDto>? CircuitPointsCollecte { get; set; }
}
