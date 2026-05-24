using CollectManagement.Application.Interfaces.Repositories.Sites;
using CollectManagement.Domain.Sites;
using CollectManagement.Domain.Sites.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Sites.Commands.CreateSite;

public class CreateSiteCommandHandler : IRequestHandler<CreateSiteCommand, CreateSiteResponse>
{
    private readonly ISiteRepository _siteRepository;
    private readonly IMapper _mapper;

    public CreateSiteCommandHandler(ISiteRepository siteRepository, IMapper mapper)
    {
        _siteRepository = siteRepository;
        _mapper = mapper;
    }

    public async Task<CreateSiteResponse> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
    {
        var siteId = new SiteId(Ulid.NewUlid());
        var site = Site.Create(siteId, request.Code, request.Site, request.Siege, request.Longitude, request.Latitude, request.Rayon, request.TimeMinute, request.IsActive, new SocieteId(request.SocieteId));

        await _siteRepository.AddAsync(site, cancellationToken).ConfigureAwait(false);
        return _mapper.Map<CreateSiteResponse>(site);
    }
}
