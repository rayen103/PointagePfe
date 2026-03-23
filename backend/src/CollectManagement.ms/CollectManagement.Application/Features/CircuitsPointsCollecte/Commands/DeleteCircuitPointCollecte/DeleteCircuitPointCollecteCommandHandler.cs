using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.DeleteCircuitPointCollecte;

public class DeleteCircuitPointCollecteCommandHandler
    : IRequestHandler<DeleteCircuitPointCollecteCommand, Unit>
{
    private readonly ICircuitPointCollecteRepository _repository;

    public DeleteCircuitPointCollecteCommandHandler(ICircuitPointCollecteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        DeleteCircuitPointCollecteCommand request,
        CancellationToken cancellationToken)
    {
        var id = new CircuitPointCollecteId(request.CircuitPointCollecteId);

        await _repository
            .DeleteAsync(c => c.CircuitPointCollecteId == id, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
