using CollectManagement.Application.Interfaces.Repositories.OrdresTravailDetails;
using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravailDetails.Commands.CreateOrdreTravailDetail;

public class CreateOrdreTravailDetailCommandHandler
    : IRequestHandler<CreateOrdreTravailDetailCommand, CreateOrdreTravailDetailResponse>
{
    private readonly IOrdreTravailDetailRepository _repository;
    private readonly IMapper _mapper;

    public CreateOrdreTravailDetailCommandHandler(
        IOrdreTravailDetailRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateOrdreTravailDetailResponse> Handle(
        CreateOrdreTravailDetailCommand request,
        CancellationToken cancellationToken)
    {
        var id = new OrdreTravailDetailId(Ulid.NewUlid());
        var ordreTravailId = new OrdreTravailId(request.OrdreTravailId);

        var entity = OrdreTravailDetail.Create(
            id,
            ordreTravailId,
            request.CodeArticle,
            request.CodeEntrepot,
            request.CodeUnite,
            request.LibelleArticle,
            request.PrixUnitaireHT,
            request.Quantite,
            request.TauxTVA,
            request.Montant
        );

        await _repository
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateOrdreTravailDetailResponse>(entity);
    }
}
