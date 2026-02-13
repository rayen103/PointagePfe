namespace CollectManagement.Application.Features.Circuits.Commands.DeleteCircuit;

public record DeleteCircuitCommand(Ulid CircuitId) : IRequest<Unit>;
