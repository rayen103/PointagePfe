namespace CollectManagement.Application.Features.Interventions.Queries.GetPagedListIntervention;

public record GetPagedListInterventionQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListInterventionResponse>;
