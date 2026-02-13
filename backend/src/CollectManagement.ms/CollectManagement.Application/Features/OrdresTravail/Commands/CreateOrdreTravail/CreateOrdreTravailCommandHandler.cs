using CollectManagement.Application.Interfaces.Repositories.OrdresTravail;
using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravail.Commands.CreateOrdreTravail;

public class CreateOrdreTravailCommandHandler
    : IRequestHandler<CreateOrdreTravailCommand, CreateOrdreTravailResponse>
{
    private readonly IOrdreTravailRepository _ordreTravailRepository;
    private readonly IMapper _mapper;

    public CreateOrdreTravailCommandHandler(
        IOrdreTravailRepository ordreTravailRepository,
        IMapper mapper)
    {
        _ordreTravailRepository = ordreTravailRepository;
        _mapper = mapper;
    }

    public async Task<CreateOrdreTravailResponse> Handle(CreateOrdreTravailCommand request, CancellationToken cancellationToken)
    {
        var ordreTravailId = new OrdreTravailId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        var ordreTravail = OrdreTravail.Create(
            ordreTravailId,
            request.NumeroOrdreTravail,
            request.NumeroChantier,
            request.CodeClient,
            request.NumeroBonCommande,
            request.CodeEquipe,
            request.EtatOT,
            request.Montant,
            request.DateCreation,
            request.NumeroConvention,
            request.CodeVehicule,
            request.Libelle,
            request.IsActive,
            societeId
        );

        await _ordreTravailRepository
            .AddAsync(ordreTravail, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateOrdreTravailResponse>(ordreTravail);
    }
}
