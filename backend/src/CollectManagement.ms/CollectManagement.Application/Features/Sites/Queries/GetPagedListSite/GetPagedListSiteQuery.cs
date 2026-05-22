namespace CollectManagement.Application.Features.Sites.Queries.GetPagedListSite;

public record GetPagedListSiteQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size,
    Ulid? SocieteId
) : IRequest<GetPagedListSiteResponse>;
