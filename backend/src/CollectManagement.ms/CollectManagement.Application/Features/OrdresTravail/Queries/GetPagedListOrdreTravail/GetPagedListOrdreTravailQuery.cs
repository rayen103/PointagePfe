namespace CollectManagement.Application.Features.OrdresTravail.Queries.GetPagedListOrdreTravail;

public record GetPagedListOrdreTravailQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListOrdreTravailResponse>;
