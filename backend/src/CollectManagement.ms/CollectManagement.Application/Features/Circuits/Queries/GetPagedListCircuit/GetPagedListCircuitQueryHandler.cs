using CollectManagement.Application.Interfaces.Repositories.Circuits;

namespace CollectManagement.Application.Features.Circuits.Queries.GetPagedListCircuit;

public class GetPagedListCircuitQueryHandler
    : IRequestHandler<GetPagedListCircuitQuery, GetPagedListCircuitResponse>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IMapper _mapper;

    public GetPagedListCircuitQueryHandler(
        ICircuitRepository circuitRepository,
        IMapper mapper)
    {
        _circuitRepository = circuitRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListCircuitResponse> Handle(GetPagedListCircuitQuery request, CancellationToken cancellationToken)
    {
        var (circuits, totalCount) = await _circuitRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListCircuitResponse
        {
            Circuits = _mapper.Map<IReadOnlyList<GetPagedListCircuitDto>>(circuits),
            TotalCount = totalCount
        };
    }
}
