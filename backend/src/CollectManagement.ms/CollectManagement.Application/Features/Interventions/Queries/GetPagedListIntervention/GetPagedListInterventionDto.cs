namespace CollectManagement.Application.Features.Interventions.Queries.GetPagedListIntervention;

public record GetPagedListInterventionDto(
    string InterventionId,
    string NumeroIntervention,
    string? Description,
    DateTime DateIntervention,
    string? TypeIntervention,
    string? Statut,
    decimal? Cout
);
