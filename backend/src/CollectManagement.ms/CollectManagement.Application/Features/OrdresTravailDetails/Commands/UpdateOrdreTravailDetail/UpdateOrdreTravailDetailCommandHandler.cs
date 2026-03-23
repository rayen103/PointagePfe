using CollectManagement.Application.Interfaces.Repositories.OrdresTravailDetails;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravailDetails.Commands.UpdateOrdreTravailDetail;

public class UpdateOrdreTravailDetailCommandHandler
    : IRequestHandler<UpdateOrdreTravailDetailCommand, UpdateOrdreTravailDetailResponse>
{
    private readonly IOrdreTravailDetailRepository _repository;
    private readonly IMapper _mapper;

    public UpdateOrdreTravailDetailCommandHandler(
        IOrdreTravailDetailRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateOrdreTravailDetailResponse> Handle(
        UpdateOrdreTravailDetailCommand request,
        CancellationToken cancellationToken)
    {
        var id = new OrdreTravailDetailId(request.OrdreTravailDetailId);

        var entity = await _repository
            .GetOneAsync(id, cancellationToken)
            .ConfigureAwait(false);

        entity.Update(
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
            .UpdateBulkAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateOrdreTravailDetailResponse>(entity);
    }
}
