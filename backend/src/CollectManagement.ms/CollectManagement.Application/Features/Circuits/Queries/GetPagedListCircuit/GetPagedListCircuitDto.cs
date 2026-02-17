namespace CollectManagement.Application.Features.Circuits.Queries.GetPagedListCircuit;

public class GetPagedListCircuitDto
{
    public Ulid CircuitId { get; set; }
    public string CodeCircuit { get; set; } = string.Empty;
    public string? LibelleCircuit { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
