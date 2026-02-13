using CollectManagement.Application.Interfaces.Repositories.Rattachements;

namespace CollectManagement.Application.Features.Rattachements.Queries.GetPagedListRattachement;

public class GetPagedListRattachementQueryHandler
    : IRequestHandler<GetPagedListRattachementQuery, GetPagedListRattachementResponse>
{
    private readonly IRattachementRepository _rattachementRepository;
    private readonly IMapper _mapper;

    public GetPagedListRattachementQueryHandler(
        IRattachementRepository rattachementRepository,
        IMapper mapper)
    {
        _rattachementRepository = rattachementRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListRattachementResponse> Handle(GetPagedListRattachementQuery request, CancellationToken cancellationToken)
    {
        var (rattachements, totalCount) = await _rattachementRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListRattachementResponse
        {
            Rattachements = _mapper.Map<IReadOnlyList<GetPagedListRattachementDto>>(rattachements),
            TotalCount = totalCount
        };
    }
}
