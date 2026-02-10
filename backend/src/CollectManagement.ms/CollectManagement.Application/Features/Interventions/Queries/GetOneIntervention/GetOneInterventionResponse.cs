namespace CollectManagement.Application.Features.Interventions.Queries.GetOneIntervention;

public record GetOneInterventionResponse(
    string InterventionId,
    string NumeroIntervention,
    string? Description,
    DateTime DateIntervention,
    string? TypeIntervention,
    string? Statut,
    decimal? Cout
);
