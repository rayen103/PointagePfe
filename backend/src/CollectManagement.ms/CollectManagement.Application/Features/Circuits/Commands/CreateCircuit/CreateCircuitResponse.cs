namespace CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;

public class CreateCircuitResponse
{
    public Ulid CircuitId { get; set; }
    public string CodeCircuit { get; set; } = string.Empty;
    public string? LibelleCircuit { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
