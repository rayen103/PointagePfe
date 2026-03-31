using CollectManagement.Application.Interfaces.Repositories.Chantiers;
using CollectManagement.Domain.Chantiers;
using CollectManagement.Domain.Chantiers.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Chantiers.Commands.CreateChantier;

public class CreateChantierCommandHandler
    : IRequestHandler<CreateChantierCommand, CreateChantierResponse>
{
    private readonly IChantierRepository _chantierRepository;
    private readonly IMapper _mapper;

    public CreateChantierCommandHandler(
        IChantierRepository chantierRepository,
        IMapper mapper)
    {
        _chantierRepository = chantierRepository;
        _mapper = mapper;
    }

    public async Task<CreateChantierResponse> Handle(CreateChantierCommand request, CancellationToken cancellationToken)
    {
        var chantierId = new ChantierId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        var chantier = Chantier.Create(
            chantierId,
            request.NumeroChantier,
            request.LibelleChantier,
            request.CodeClient,
            request.Adresse,
            request.MontantHT,
            request.MontantTTC,
            request.Nature,
            request.Responsable,
            request.DateDebut,
            request.DateFin,
            request.Status,
            request.IsActive,
            societeId);

        await _chantierRepository
            .AddAsync(chantier, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateChantierResponse>(chantier);
    }
}
