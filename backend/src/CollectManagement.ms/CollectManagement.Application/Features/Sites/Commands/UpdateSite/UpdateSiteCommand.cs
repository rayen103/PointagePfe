namespace CollectManagement.Application.Features.Sites.Commands.UpdateSite;

public record UpdateSiteCommand(
    Ulid SiteId,
    string Code,
    string Site,
    bool Siege,
    decimal? Longitude,
    decimal? Latitude,
    decimal? Rayon,
    int? TimeMinute,
    bool IsActive
) : IRequest<UpdateSiteResponse>;
