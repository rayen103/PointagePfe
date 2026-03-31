namespace CollectManagement.Application.Features.Chantiers.Queries.GetPagedListChantier;

public class GetPagedListChantierResponse
{
    public IReadOnlyList<GetPagedListChantierDto> Chantiers { get; set; } = new List<GetPagedListChantierDto>();
    public int TotalCount { get; set; }
}
