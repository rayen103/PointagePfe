using CollectManagement.Application.Interfaces.Repositories.Sites;
using CollectManagement.Domain.Sites.ValueObjects;

namespace CollectManagement.Application.Features.Sites.Commands.UpdateSite;

public class UpdateSiteCommandHandler : IRequestHandler<UpdateSiteCommand, UpdateSiteResponse>
{
    private readonly ISiteRepository _siteRepository;
    private readonly IMapper _mapper;

    public UpdateSiteCommandHandler(ISiteRepository siteRepository, IMapper mapper)
    {
        _siteRepository = siteRepository;
        _mapper = mapper;
    }

    public async Task<UpdateSiteResponse> Handle(UpdateSiteCommand request, CancellationToken cancellationToken)
    {
        var site = await _siteRepository.GetOneAsync(new SiteId(request.SiteId), cancellationToken).ConfigureAwait(false);
        site.Update(request.Code, request.Site, request.Siege, request.Longitude, request.Latitude, request.Rayon, request.TimeMinute, request.IsActive);
        await _siteRepository.UpdateBulkAsync(site, cancellationToken).ConfigureAwait(false);
        return _mapper.Map<UpdateSiteResponse>(site);
    }
}
