using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.RattachementArticles.Commands.UpdateRattachementArticle;

public class UpdateRattachementArticleCommandHandler
    : IRequestHandler<UpdateRattachementArticleCommand, UpdateRattachementArticleResponse>
{
    private readonly IRattachementArticleRepository _rattachementArticleRepository;
    private readonly IMapper _mapper;

    public UpdateRattachementArticleCommandHandler(
        IRattachementArticleRepository rattachementArticleRepository,
        IMapper mapper)
    {
        _rattachementArticleRepository = rattachementArticleRepository;
        _mapper = mapper;
    }

    public async Task<UpdateRattachementArticleResponse> Handle(
        UpdateRattachementArticleCommand request,
        CancellationToken cancellationToken)
    {
        var rattachementArticleId = new RattachementArticleId(request.RattachementArticleId);
        var rattachementId = new RattachementId(request.RattachementId);

        var rattachementArticle = await _rattachementArticleRepository
            .GetOneAsync(rattachementArticleId, cancellationToken)
            .ConfigureAwait(false);

        rattachementArticle.Update(
            rattachementId,
            request.CodeArticle,
            request.Libelle,
            request.Quantite,
            request.PrixRevient,
            request.TauxTVA,
            request.CodeUnite,
            request.CodeEntrepot,
            request.TypeRattachement,
            request.NumeroBonLivraison,
            request.DateBonLivraison,
            request.IsActive);

        await _rattachementArticleRepository
            .UpdateBulkAsync(rattachementArticle, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateRattachementArticleResponse>(rattachementArticle);
    }
}
