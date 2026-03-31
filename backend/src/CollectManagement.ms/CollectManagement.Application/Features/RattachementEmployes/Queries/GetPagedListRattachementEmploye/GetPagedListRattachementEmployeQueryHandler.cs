using CollectManagement.Application.Interfaces.Repositories.Rattachements;

namespace CollectManagement.Application.Features.RattachementEmployes.Queries.GetPagedListRattachementEmploye;

public class GetPagedListRattachementEmployeQueryHandler
    : IRequestHandler<GetPagedListRattachementEmployeQuery, GetPagedListRattachementEmployeResponse>
{
    private readonly IRattachementEmployeRepository _rattachementEmployeRepository;
    private readonly IMapper _mapper;

    public GetPagedListRattachementEmployeQueryHandler(
        IRattachementEmployeRepository rattachementEmployeRepository,
        IMapper mapper)
    {
        _rattachementEmployeRepository = rattachementEmployeRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListRattachementEmployeResponse> Handle(
        GetPagedListRattachementEmployeQuery request,
        CancellationToken cancellationToken)
    {
        var (rattachementEmployes, totalCount) = await _rattachementEmployeRepository
            .GetPagedListAsync(request.Search, request.Sort, request.Order, request.Page, request.Size, cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListRattachementEmployeResponse
        {
            RattachementEmployes = _mapper.Map<IReadOnlyList<GetPagedListRattachementEmployeDto>>(rattachementEmployes),
            TotalCount = totalCount
        };
    }
}
