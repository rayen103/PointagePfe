using CollectManagement.Application.Interfaces.Repositories.Sites;
using CollectManagement.Domain.Sites.ValueObjects;

namespace CollectManagement.Application.Features.Sites.Commands.DeleteSite;

public class DeleteSiteCommandHandler : IRequestHandler<DeleteSiteCommand>
{
    private readonly ISiteRepository _siteRepository;

    public DeleteSiteCommandHandler(ISiteRepository siteRepository)
    {
        _siteRepository = siteRepository;
    }

    public async Task Handle(DeleteSiteCommand request, CancellationToken cancellationToken)
    {
        await _siteRepository.DeleteAsync(x => x.SiteId == new SiteId(request.SiteId), cancellationToken).ConfigureAwait(false);
    }
}
