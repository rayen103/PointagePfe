namespace CollectManagement.Application.Features.Interventions.Queries.GetPagedListIntervention;

public record GetPagedListInterventionResponse(
    List<GetPagedListInterventionDto> interventions,
    int total
);
