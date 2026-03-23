namespace CollectManagement.Application.Features.Shifts.Queries.GetPagedListShift;

public class GetPagedListShiftResponse
{
    public IReadOnlyList<GetPagedListShiftDto> Shifts { get; set; } = new List<GetPagedListShiftDto>();
    public int TotalCount { get; set; }
}
