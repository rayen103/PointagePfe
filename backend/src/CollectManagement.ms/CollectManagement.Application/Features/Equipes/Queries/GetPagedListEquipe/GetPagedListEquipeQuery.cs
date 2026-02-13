namespace CollectManagement.Application.Features.Equipes.Queries.GetPagedListEquipe;

public record GetPagedListEquipeQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListEquipeResponse>;
