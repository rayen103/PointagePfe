using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Domain.PointsCollecte.ValueObjects;

namespace CollectManagement.Application.Features.PointsCollecte.Commands.UpdatePointCollecte;

public class UpdatePointCollecteCommandHandler
    : IRequestHandler<UpdatePointCollecteCommand, UpdatePointCollecteResponse>
{
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly IMapper _mapper;

    public UpdatePointCollecteCommandHandler(
        IPointCollecteRepository pointCollecteRepository,
        IMapper mapper)
    {
        _pointCollecteRepository = pointCollecteRepository;
        _mapper = mapper;
    }

    public async Task<UpdatePointCollecteResponse> Handle(UpdatePointCollecteCommand request, CancellationToken cancellationToken)
    {
        var pointCollecteId = new PointCollecteId(request.PointCollecteId);

        var pointCollecte = await _pointCollecteRepository
            .GetOneAsync(pointCollecteId, cancellationToken)
            .ConfigureAwait(false);

        var circuitId = request.CircuitId != null ? new CircuitId(request.CircuitId.Value) : null;

        pointCollecte.Update(
            request.CodePointCollecte,
            request.LibellePointCollecte,
            request.Latitude,
            request.Longitude,
            request.CodeGouvernorat,
            request.CodeRegion,
            request.IsActive,
            circuitId
        );

        await _pointCollecteRepository
            .UpdateBulkAsync(pointCollecte, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdatePointCollecteResponse>(pointCollecte);
    }
}
