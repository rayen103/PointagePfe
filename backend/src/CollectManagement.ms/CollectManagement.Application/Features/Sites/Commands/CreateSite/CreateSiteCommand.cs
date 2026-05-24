namespace CollectManagement.Application.Features.Sites.Commands.CreateSite;

public record CreateSiteCommand(
    string Code,
    string Site,
    bool Siege,
    decimal? Longitude,
    decimal? Latitude,
    decimal? Rayon,
    int? TimeMinute,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateSiteResponse>;
