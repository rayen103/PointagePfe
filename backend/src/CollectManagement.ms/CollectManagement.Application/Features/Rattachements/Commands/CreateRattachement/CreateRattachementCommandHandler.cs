using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Rattachements.Commands.CreateRattachement;

public class CreateRattachementCommandHandler
    : IRequestHandler<CreateRattachementCommand, CreateRattachementResponse>
{
    private readonly IRattachementRepository _rattachementRepository;
    private readonly IMapper _mapper;

    public CreateRattachementCommandHandler(
        IRattachementRepository rattachementRepository,
        IMapper mapper)
    {
        _rattachementRepository = rattachementRepository;
        _mapper = mapper;
    }

    public async Task<CreateRattachementResponse> Handle(CreateRattachementCommand request, CancellationToken cancellationToken)
    {
        var rattachementId = new RattachementId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        var rattachement = Rattachement.Create(
            rattachementId,
            request.NumeroRattachement,
            request.Exercice,
            request.DateRattachement,
            request.NumeroChantier,
            request.CodeClient,
            request.IsInternal,
            request.Cout,
            request.Type,
            request.Nature,
            request.Responsable,
            request.HeureDebut,
            request.HeureFin,
            request.Emplacement,
            request.Reference,
            request.Status,
            request.DateCloture,
            request.Remarque,
            request.IsActive,
            societeId
        );

        await _rattachementRepository
            .AddAsync(rattachement, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateRattachementResponse>(rattachement);
    }
}
