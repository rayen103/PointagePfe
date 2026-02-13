namespace CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;

public record CreateCircuitCommand(
    string CodeCircuit,
    string? LibelleCircuit,
    string? Description,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateCircuitResponse>;
