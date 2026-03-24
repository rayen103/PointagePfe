namespace CollectManagement.Application.Features.RattachementEmployes.Queries.GetPagedListRattachementEmploye;

public record GetPagedListRattachementEmployeQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListRattachementEmployeResponse>;
