namespace CollectManagement.Application.Features.Chantiers.Queries.GetPagedListChantier;

public record GetPagedListChantierQuery(string? Search, string? Sort, string? Order, int Page, int Size)
    : IRequest<GetPagedListChantierResponse>;
