namespace CollectManagement.Application.Features.Interventions.Queries.GetOneIntervention;

public record GetOneInterventionQuery(
    Ulid InterventionId
) : IRequest<GetOneInterventionResponse>;
