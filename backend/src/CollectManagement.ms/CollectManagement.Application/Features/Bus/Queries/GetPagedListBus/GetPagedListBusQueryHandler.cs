using CollectManagement.Application.Interfaces.Repositories.Bus;

namespace CollectManagement.Application.Features.Bus.Queries.GetPagedListBus;

public class GetPagedListBusQueryHandler
    : IRequestHandler<GetPagedListBusQuery, GetPagedListBusResponse>
{
    private readonly IBusRepository _busRepository;
    private readonly IMapper _mapper;

    public GetPagedListBusQueryHandler(
        IBusRepository busRepository,
        IMapper mapper)
    {
        _busRepository = busRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListBusResponse> Handle(GetPagedListBusQuery request, CancellationToken cancellationToken)
    {
        var (buses, totalCount) = await _busRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListBusResponse
        {
            Buses = _mapper.Map<IReadOnlyList<GetPagedListBusDto>>(buses),
            TotalCount = totalCount
        };
    }
}
