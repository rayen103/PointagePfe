namespace CollectManagement.Application.Features.Bus.Queries.GetPagedListBus;

public class GetPagedListBusResponse
{
    public IReadOnlyList<GetPagedListBusDto> Buses { get; set; } = new List<GetPagedListBusDto>();
    public int TotalCount { get; set; }
}
