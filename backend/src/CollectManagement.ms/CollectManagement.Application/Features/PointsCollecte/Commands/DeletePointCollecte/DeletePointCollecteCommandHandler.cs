using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;

namespace CollectManagement.Application.Features.PointsCollecte.Commands.DeletePointCollecte;

public class DeletePointCollecteCommandHandler
    : IRequestHandler<DeletePointCollecteCommand, Unit>
{
    private readonly IPointCollecteRepository _pointCollecteRepository;

    public DeletePointCollecteCommandHandler(IPointCollecteRepository pointCollecteRepository)
    {
        _pointCollecteRepository = pointCollecteRepository;
    }

    public async Task<Unit> Handle(DeletePointCollecteCommand request, CancellationToken cancellationToken)
    {
        var pointCollecteId = new PointCollecteId(request.PointCollecteId);

        await _pointCollecteRepository
            .DeleteAsync(c => c.PointCollecteId == pointCollecteId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
