namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Queries.GetByCircuit;

public record GetByCircuitQuery(Ulid CircuitId) : IRequest<GetByCircuitResponse>;
