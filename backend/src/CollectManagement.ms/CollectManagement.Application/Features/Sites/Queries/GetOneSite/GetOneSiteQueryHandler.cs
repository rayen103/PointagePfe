using CollectManagement.Application.Interfaces.Repositories.Sites;
using CollectManagement.Domain.Sites.ValueObjects;

namespace CollectManagement.Application.Features.Sites.Queries.GetOneSite;

public class GetOneSiteQueryHandler : IRequestHandler<GetOneSiteQuery, GetOneSiteDto>
{
    private readonly ISiteRepository _siteRepository;
    private readonly IMapper _mapper;

    public GetOneSiteQueryHandler(ISiteRepository siteRepository, IMapper mapper)
    {
        _siteRepository = siteRepository;
        _mapper = mapper;
    }

    public async Task<GetOneSiteDto> Handle(GetOneSiteQuery request, CancellationToken cancellationToken)
    {
        var site = await _siteRepository.GetOneAsync(new SiteId(request.SiteId), cancellationToken).ConfigureAwait(false);
        return _mapper.Map<GetOneSiteDto>(site);
    }
}
