namespace CollectManagement.Application.Features.Interventions.Commands.DeleteIntervention;

public record DeleteInterventionCommand(string InterventionId) : IRequest<Unit>;
