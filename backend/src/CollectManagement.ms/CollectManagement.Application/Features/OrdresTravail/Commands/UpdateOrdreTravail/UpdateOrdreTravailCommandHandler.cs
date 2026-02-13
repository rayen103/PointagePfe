using CollectManagement.Application.Interfaces.Repositories.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravail.Commands.UpdateOrdreTravail;

public class UpdateOrdreTravailCommandHandler
    : IRequestHandler<UpdateOrdreTravailCommand, UpdateOrdreTravailResponse>
{
    private readonly IOrdreTravailRepository _ordreTravailRepository;
    private readonly IMapper _mapper;

    public UpdateOrdreTravailCommandHandler(
        IOrdreTravailRepository ordreTravailRepository,
        IMapper mapper)
    {
        _ordreTravailRepository = ordreTravailRepository;
        _mapper = mapper;
    }

    public async Task<UpdateOrdreTravailResponse> Handle(UpdateOrdreTravailCommand request, CancellationToken cancellationToken)
    {
        var ordreTravailId = new OrdreTravailId(request.OrdreTravailId);

        var ordreTravail = await _ordreTravailRepository
            .GetOneAsync(ordreTravailId, cancellationToken)
            .ConfigureAwait(false);

        ordreTravail.Update(
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
            request.IsActive
        );

        await _ordreTravailRepository
            .UpdateBulkAsync(ordreTravail, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateOrdreTravailResponse>(ordreTravail);
    }
}
