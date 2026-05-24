namespace CollectManagement.Application.Features.Sites.Queries.GetOneSite;

public record GetOneSiteQuery(Ulid SiteId) : IRequest<GetOneSiteDto>;
