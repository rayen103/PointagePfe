using CollectManagement.Application.Interfaces.Repositories.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;

namespace CollectManagement.Application.Features.Equipes.Commands.DeleteEquipe;

public class DeleteEquipeCommandHandler
    : IRequestHandler<DeleteEquipeCommand, Unit>
{
    private readonly IEquipeRepository _equipeRepository;

    public DeleteEquipeCommandHandler(IEquipeRepository equipeRepository)
    {
        _equipeRepository = equipeRepository;
    }

    public async Task<Unit> Handle(DeleteEquipeCommand request, CancellationToken cancellationToken)
    {
        var equipeId = new EquipeId(request.EquipeId);

        await _equipeRepository
            .DeleteAsync(c => c.EquipeId == equipeId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
