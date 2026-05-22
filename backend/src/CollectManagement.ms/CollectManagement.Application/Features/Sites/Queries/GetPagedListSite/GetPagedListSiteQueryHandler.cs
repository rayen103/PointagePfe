using CollectManagement.Application.Interfaces.Repositories.Sites;

namespace CollectManagement.Application.Features.Sites.Queries.GetPagedListSite;

public class GetPagedListSiteQueryHandler : IRequestHandler<GetPagedListSiteQuery, GetPagedListSiteResponse>
{
    private readonly ISiteRepository _siteRepository;
    private readonly IMapper _mapper;

    public GetPagedListSiteQueryHandler(ISiteRepository siteRepository, IMapper mapper)
    {
        _siteRepository = siteRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListSiteResponse> Handle(GetPagedListSiteQuery request, CancellationToken cancellationToken)
    {
        var (sites, totalCount) = await _siteRepository.GetPagedListAsync(request.Search, request.Sort, request.Order, request.Page, request.Size, request.SocieteId, cancellationToken).ConfigureAwait(false);
        return new GetPagedListSiteResponse(_mapper.Map<IReadOnlyList<GetPagedListSiteDto>>(sites), totalCount);
    }
}
