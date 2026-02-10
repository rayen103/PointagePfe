namespace CollectManagement.Application.Features.Interventions.Commands.CreateIntervention;

public record CreateInterventionResponse(
    string InterventionId,
    string NumeroIntervention,
    string? Description,
    DateTime DateIntervention,
    string? TypeIntervention,
    string? Statut,
    decimal? Cout
);
