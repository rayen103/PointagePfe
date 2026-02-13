namespace CollectManagement.Application.Features.Equipes.Queries.GetPagedListEquipe;

public class GetPagedListEquipeResponse
{
    public IReadOnlyList<GetPagedListEquipeDto> Equipes { get; set; } = new List<GetPagedListEquipeDto>();
    public int TotalCount { get; set; }
}
