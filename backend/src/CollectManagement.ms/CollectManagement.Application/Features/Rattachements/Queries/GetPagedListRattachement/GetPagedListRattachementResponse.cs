namespace CollectManagement.Application.Features.Rattachements.Queries.GetPagedListRattachement;

public class GetPagedListRattachementResponse
{
    public IReadOnlyList<GetPagedListRattachementDto> Rattachements { get; set; } = new List<GetPagedListRattachementDto>();
    public int TotalCount { get; set; }
}
