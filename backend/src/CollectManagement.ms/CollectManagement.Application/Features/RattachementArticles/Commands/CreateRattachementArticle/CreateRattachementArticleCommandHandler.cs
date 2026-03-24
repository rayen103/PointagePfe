using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.RattachementArticles.Commands.CreateRattachementArticle;

public class CreateRattachementArticleCommandHandler
    : IRequestHandler<CreateRattachementArticleCommand, CreateRattachementArticleResponse>
{
    private readonly IRattachementArticleRepository _rattachementArticleRepository;
    private readonly IMapper _mapper;

    public CreateRattachementArticleCommandHandler(
        IRattachementArticleRepository rattachementArticleRepository,
        IMapper mapper)
    {
        _rattachementArticleRepository = rattachementArticleRepository;
        _mapper = mapper;
    }

    public async Task<CreateRattachementArticleResponse> Handle(
        CreateRattachementArticleCommand request,
        CancellationToken cancellationToken)
    {
        var rattachementArticleId = new RattachementArticleId(Ulid.NewUlid());
        var rattachementId = new RattachementId(request.RattachementId);
        var societeId = new SocieteId(request.SocieteId);

        var rattachementArticle = RattachementArticle.Create(
            rattachementArticleId,
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
            request.IsActive,
            societeId);

        await _rattachementArticleRepository
            .AddAsync(rattachementArticle, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateRattachementArticleResponse>(rattachementArticle);
    }
}
