using CollectManagement.Application.Features.Sites.Commands.CreateSite;
using CollectManagement.Application.Features.Sites.Commands.UpdateSite;
using CollectManagement.Application.Features.Sites.Queries.GetOneSite;
using CollectManagement.Application.Features.Sites.Queries.GetPagedListSite;
using CollectManagement.Domain.Sites;

namespace CollectManagement.Application.Features.Sites.Mapping;

public class SiteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Site, CreateSiteResponse>()
            .Map(d => d.SiteId, s => s.SiteId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Site, UpdateSiteResponse>()
            .Map(d => d.SiteId, s => s.SiteId.Value);

        config.NewConfig<Site, GetOneSiteDto>()
            .Map(d => d.SiteId, s => s.SiteId.Value)
            .Map(d => d.Site, s => s.LibelleSite)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Site, GetPagedListSiteDto>()
            .Map(d => d.SiteId, s => s.SiteId.Value)
            .Map(d => d.Site, s => s.LibelleSite)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
