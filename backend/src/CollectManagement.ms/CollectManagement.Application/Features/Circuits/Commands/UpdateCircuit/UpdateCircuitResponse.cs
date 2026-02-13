namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public class UpdateCircuitResponse
{
    public Ulid CircuitId { get; set; }
    public string CodeCircuit { get; set; } = string.Empty;
    public string? LibelleCircuit { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
