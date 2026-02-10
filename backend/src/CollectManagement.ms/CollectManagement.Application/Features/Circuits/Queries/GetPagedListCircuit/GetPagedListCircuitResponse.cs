namespace CollectManagement.Application.Features.Circuits.Queries.GetPagedListCircuit;

public class GetPagedListCircuitResponse
{
    public IReadOnlyList<GetPagedListCircuitDto> Circuits { get; set; } = new List<GetPagedListCircuitDto>();
    public int TotalCount { get; set; }
}
