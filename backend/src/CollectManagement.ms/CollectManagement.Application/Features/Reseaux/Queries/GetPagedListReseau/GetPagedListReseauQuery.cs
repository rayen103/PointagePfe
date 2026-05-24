namespace CollectManagement.Application.Features.Reseaux.Queries.GetPagedListReseau;

public record GetPagedListReseauQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size,
    Ulid? SocieteId
) : IRequest<GetPagedListReseauResponse>;
