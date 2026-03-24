namespace CollectManagement.Application.Features.RattachementEmployes.Queries.GetPagedListRattachementEmploye;

public class GetPagedListRattachementEmployeResponse
{
    public IReadOnlyList<GetPagedListRattachementEmployeDto> RattachementEmployes { get; set; } = [];
    public int TotalCount { get; set; }
}
