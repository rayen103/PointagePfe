namespace CollectManagement.Application.Features.Interventions.Commands.CreateIntervention;

public record CreateInterventionCommand(
    string NumeroIntervention,
    string? Description,
    DateTime DateIntervention,
    string? TypeIntervention,
    string? Statut,
    decimal? Cout
) : IRequest<CreateInterventionResponse>;
