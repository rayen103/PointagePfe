namespace CollectManagement.Application.Features.Rattachements.Queries.GetPagedListRattachement;

public record GetPagedListRattachementQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListRattachementResponse>;
