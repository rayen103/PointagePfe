using CollectManagement.Application.Interfaces.Repositories.Chantiers;

namespace CollectManagement.Application.Features.Chantiers.Queries.GetPagedListChantier;

public class GetPagedListChantierQueryHandler
    : IRequestHandler<GetPagedListChantierQuery, GetPagedListChantierResponse>
{
    private readonly IChantierRepository _chantierRepository;
    private readonly IMapper _mapper;

    public GetPagedListChantierQueryHandler(IChantierRepository chantierRepository, IMapper mapper)
    {
        _chantierRepository = chantierRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListChantierResponse> Handle(GetPagedListChantierQuery request, CancellationToken cancellationToken)
    {
        var (chantiers, totalCount) = await _chantierRepository
            .GetPagedListAsync(request.Search, request.Sort, request.Order, request.Page, request.Size, cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListChantierResponse
        {
            Chantiers = _mapper.Map<IReadOnlyList<GetPagedListChantierDto>>(chantiers),
            TotalCount = totalCount
        };
    }
}
