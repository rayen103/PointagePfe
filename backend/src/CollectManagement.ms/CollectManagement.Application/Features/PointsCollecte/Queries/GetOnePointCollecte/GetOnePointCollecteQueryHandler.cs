using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;

namespace CollectManagement.Application.Features.PointsCollecte.Queries.GetOnePointCollecte;

public class GetOnePointCollecteQueryHandler
    : IRequestHandler<GetOnePointCollecteQuery, GetOnePointCollecteDto>
{
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly IMapper _mapper;

    public GetOnePointCollecteQueryHandler(
        IPointCollecteRepository pointCollecteRepository,
        IMapper mapper)
    {
        _pointCollecteRepository = pointCollecteRepository;
        _mapper = mapper;
    }

    public async Task<GetOnePointCollecteDto> Handle(GetOnePointCollecteQuery request, CancellationToken cancellationToken)
    {
        var pointCollecteId = new PointCollecteId(request.PointCollecteId);

        var pointCollecte = await _pointCollecteRepository
            .GetOneAsync(pointCollecteId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOnePointCollecteDto>(pointCollecte);
    }
}
