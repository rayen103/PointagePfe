using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Interventions;
using CollectManagement.Domain.Interventions.ValueObjects;

namespace CollectManagement.Application.Features.Interventions.Commands.UpdateIntervention;

public class UpdateInterventionCommandHandler : IRequestHandler<UpdateInterventionCommand, Unit>
{
    private readonly IInterventionRepository _interventionRepository;

    public UpdateInterventionCommandHandler(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }

    public async Task<Unit> Handle(UpdateInterventionCommand request, CancellationToken cancellationToken)
    {
        var interventionId = new InterventionId(Ulid.Parse(request.InterventionId));

        var intervention = await _interventionRepository
            .GetByIdAsync(interventionId)
            .ConfigureAwait(false);

        if (intervention is null)
        {
            throw new NotFoundException(nameof(intervention), request.InterventionId);
        }

        intervention.Update(
            request.NumeroIntervention,
            request.Description,
            request.DateIntervention,
            request.TypeIntervention,
            request.Statut,
            request.Cout
        );

        _interventionRepository.Update(intervention);

        return Unit.Value;
    }
}
