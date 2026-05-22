namespace CollectManagement.Application.Features.Reseaux.Queries.GetPagedListReseau;

public record GetPagedListReseauResponse(IReadOnlyList<GetPagedListReseauDto> Reseaux, int TotalCount);
