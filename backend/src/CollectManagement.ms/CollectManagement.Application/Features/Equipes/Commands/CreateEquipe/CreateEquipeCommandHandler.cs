using CollectManagement.Application.Interfaces.Repositories.Equipes;
using CollectManagement.Domain.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Equipes.Commands.CreateEquipe;

public class CreateEquipeCommandHandler
    : IRequestHandler<CreateEquipeCommand, CreateEquipeResponse>
{
    private readonly IEquipeRepository _equipeRepository;
    private readonly IMapper _mapper;

    public CreateEquipeCommandHandler(
        IEquipeRepository equipeRepository,
        IMapper mapper)
    {
        _equipeRepository = equipeRepository;
        _mapper = mapper;
    }

    public async Task<CreateEquipeResponse> Handle(CreateEquipeCommand request, CancellationToken cancellationToken)
    {
        var equipeId = new EquipeId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        var equipe = Equipe.Create(
            equipeId,
            request.CodeEquipe,
            request.LibelleEquipe,
            request.CodeClient,
            request.CodeEntrepot,
            request.CodeTarif,
            request.CodeFournisseur,
            request.Responsable,
            request.IsInternal,
            request.CodeVehicule,
            request.IsActive,
            societeId
        );

        await _equipeRepository
            .AddAsync(equipe, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateEquipeResponse>(equipe);
    }
}
