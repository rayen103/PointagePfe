namespace CollectManagement.Application.Features.PointsCollecte.Queries.GetPagedListPointCollecte;

public record GetPagedListPointCollecteQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListPointCollecteResponse>;
