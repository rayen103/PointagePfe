using CollectManagement.Application.Interfaces.Repositories.OrdresTravail;

namespace CollectManagement.Application.Features.OrdresTravail.Queries.GetPagedListOrdreTravail;

public class GetPagedListOrdreTravailQueryHandler
    : IRequestHandler<GetPagedListOrdreTravailQuery, GetPagedListOrdreTravailResponse>
{
    private readonly IOrdreTravailRepository _ordreTravailRepository;
    private readonly IMapper _mapper;

    public GetPagedListOrdreTravailQueryHandler(
        IOrdreTravailRepository ordreTravailRepository,
        IMapper mapper)
    {
        _ordreTravailRepository = ordreTravailRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListOrdreTravailResponse> Handle(GetPagedListOrdreTravailQuery request, CancellationToken cancellationToken)
    {
        var (ordresTravail, totalCount) = await _ordreTravailRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListOrdreTravailResponse
        {
            OrdresTravail = _mapper.Map<IReadOnlyList<GetPagedListOrdreTravailDto>>(ordresTravail),
            TotalCount = totalCount
        };
    }
}
