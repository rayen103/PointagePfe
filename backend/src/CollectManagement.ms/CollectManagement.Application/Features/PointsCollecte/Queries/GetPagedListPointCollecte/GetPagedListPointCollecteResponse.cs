namespace CollectManagement.Application.Features.PointsCollecte.Queries.GetPagedListPointCollecte;

public class GetPagedListPointCollecteResponse
{
    public IReadOnlyList<GetPagedListPointCollecteDto> PointsCollecte { get; set; } = new List<GetPagedListPointCollecteDto>();
    public int TotalCount { get; set; }
}
