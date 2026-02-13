using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;

namespace CollectManagement.Application.Features.PointsCollecte.Queries.GetPagedListPointCollecte;

public class GetPagedListPointCollecteQueryHandler
    : IRequestHandler<GetPagedListPointCollecteQuery, GetPagedListPointCollecteResponse>
{
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly IMapper _mapper;

    public GetPagedListPointCollecteQueryHandler(
        IPointCollecteRepository pointCollecteRepository,
        IMapper mapper)
    {
        _pointCollecteRepository = pointCollecteRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListPointCollecteResponse> Handle(GetPagedListPointCollecteQuery request, CancellationToken cancellationToken)
    {
        var (pointsCollecte, totalCount) = await _pointCollecteRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListPointCollecteResponse
        {
            PointsCollecte = _mapper.Map<IReadOnlyList<GetPagedListPointCollecteDto>>(pointsCollecte),
            TotalCount = totalCount
        };
    }
}
