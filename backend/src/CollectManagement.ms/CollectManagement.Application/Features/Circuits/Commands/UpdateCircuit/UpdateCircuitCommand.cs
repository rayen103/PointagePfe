namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public record UpdateCircuitCommand(
    Ulid CircuitId,
    string CodeCircuit,
    string? LibelleCircuit,
    string? Description,
    bool IsActive
) : IRequest<UpdateCircuitResponse>;
