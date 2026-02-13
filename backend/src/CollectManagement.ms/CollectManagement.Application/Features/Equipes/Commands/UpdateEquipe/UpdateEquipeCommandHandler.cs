using CollectManagement.Application.Interfaces.Repositories.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;

namespace CollectManagement.Application.Features.Equipes.Commands.UpdateEquipe;

public class UpdateEquipeCommandHandler
    : IRequestHandler<UpdateEquipeCommand, UpdateEquipeResponse>
{
    private readonly IEquipeRepository _equipeRepository;
    private readonly IMapper _mapper;

    public UpdateEquipeCommandHandler(
        IEquipeRepository equipeRepository,
        IMapper mapper)
    {
        _equipeRepository = equipeRepository;
        _mapper = mapper;
    }

    public async Task<UpdateEquipeResponse> Handle(UpdateEquipeCommand request, CancellationToken cancellationToken)
    {
        var equipeId = new EquipeId(request.EquipeId);

        var equipe = await _equipeRepository
            .GetOneAsync(equipeId, cancellationToken)
            .ConfigureAwait(false);

        equipe.Update(
            request.CodeEquipe,
            request.LibelleEquipe,
            request.CodeClient,
            request.CodeEntrepot,
            request.CodeTarif,
            request.CodeFournisseur,
            request.Responsable,
            request.IsInternal,
            request.CodeVehicule,
            request.IsActive
        );

        await _equipeRepository
            .UpdateBulkAsync(equipe, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateEquipeResponse>(equipe);
    }
}
