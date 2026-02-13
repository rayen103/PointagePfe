namespace CollectManagement.Application.Features.OrdresTravail.Queries.GetPagedListOrdreTravail;

public class GetPagedListOrdreTravailResponse
{
    public IReadOnlyList<GetPagedListOrdreTravailDto> OrdresTravail { get; set; } = new List<GetPagedListOrdreTravailDto>();
    public int TotalCount { get; set; }
}
