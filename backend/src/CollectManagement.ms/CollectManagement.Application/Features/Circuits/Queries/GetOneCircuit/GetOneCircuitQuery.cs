namespace CollectManagement.Application.Features.Circuits.Queries.GetOneCircuit;

public record GetOneCircuitQuery(Ulid CircuitId) : IRequest<GetOneCircuitDto>;
