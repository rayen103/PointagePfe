namespace CollectManagement.Application.Features.Interventions.Commands.UpdateIntervention;

public record UpdateInterventionCommand(
    string InterventionId,
    string NumeroIntervention,
    string? Description,
    DateTime DateIntervention,
    string? TypeIntervention,
    string? Statut,
    decimal? Cout
) : IRequest<Unit>;
