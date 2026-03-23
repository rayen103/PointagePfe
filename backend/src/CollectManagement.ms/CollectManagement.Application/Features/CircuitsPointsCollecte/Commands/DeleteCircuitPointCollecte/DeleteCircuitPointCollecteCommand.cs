namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.DeleteCircuitPointCollecte;

public record DeleteCircuitPointCollecteCommand(Ulid CircuitPointCollecteId) : IRequest<Unit>;
