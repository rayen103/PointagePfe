using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Interventions;
using CollectManagement.Domain.Interventions.ValueObjects;

namespace CollectManagement.Application.Features.Interventions.Commands.DeleteIntervention;

public class DeleteInterventionCommandHandler : IRequestHandler<DeleteInterventionCommand, Unit>
{
    private readonly IInterventionRepository _interventionRepository;

    public DeleteInterventionCommandHandler(IInterventionRepository interventionRepository)
    {
        _interventionRepository = interventionRepository;
    }

    public async Task<Unit> Handle(DeleteInterventionCommand request, CancellationToken cancellationToken)
    {
        var interventionId = new InterventionId(Ulid.Parse(request.InterventionId));

        var intervention = await _interventionRepository
            .GetByIdAsync(interventionId)
            .ConfigureAwait(false);

        if (intervention is null)
        {
            throw new NotFoundException(nameof(intervention), request.InterventionId);
        }

        await _interventionRepository
            .DeleteAsync(i => i.InterventionId == interventionId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
