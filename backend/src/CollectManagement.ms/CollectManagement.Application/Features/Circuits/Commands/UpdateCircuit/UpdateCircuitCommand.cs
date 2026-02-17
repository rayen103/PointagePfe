namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public record UpdateCircuitCommand(
    Ulid CircuitId,
    string CodeCircuit,
    string? LibelleCircuit,
    string? Description,
    double? Latitude,
    double? Longitude,
    bool IsActive
) : IRequest<UpdateCircuitResponse>;
