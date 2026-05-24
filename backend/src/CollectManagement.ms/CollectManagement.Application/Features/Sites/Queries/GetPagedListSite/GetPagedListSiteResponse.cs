namespace CollectManagement.Application.Features.Sites.Queries.GetPagedListSite;

public record GetPagedListSiteResponse(IReadOnlyList<GetPagedListSiteDto> Sites, int TotalCount);
