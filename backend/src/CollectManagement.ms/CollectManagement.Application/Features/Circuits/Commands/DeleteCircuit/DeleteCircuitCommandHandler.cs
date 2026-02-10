using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Commands.DeleteCircuit;

public class DeleteCircuitCommandHandler
    : IRequestHandler<DeleteCircuitCommand, Unit>
{
    private readonly ICircuitRepository _circuitRepository;

    public DeleteCircuitCommandHandler(ICircuitRepository circuitRepository)
    {
        _circuitRepository = circuitRepository;
    }

    public async Task<Unit> Handle(DeleteCircuitCommand request, CancellationToken cancellationToken)
    {
        var circuitId = new CircuitId(request.CircuitId);

        await _circuitRepository
            .DeleteAsync(c => c.CircuitId == circuitId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
