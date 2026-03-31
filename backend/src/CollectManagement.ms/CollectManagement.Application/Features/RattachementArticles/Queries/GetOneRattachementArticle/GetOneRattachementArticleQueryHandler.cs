using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.RattachementArticles.Queries.GetOneRattachementArticle;

public class GetOneRattachementArticleQueryHandler
    : IRequestHandler<GetOneRattachementArticleQuery, GetOneRattachementArticleDto>
{
    private readonly IRattachementArticleRepository _rattachementArticleRepository;
    private readonly IMapper _mapper;

    public GetOneRattachementArticleQueryHandler(
        IRattachementArticleRepository rattachementArticleRepository,
        IMapper mapper)
    {
        _rattachementArticleRepository = rattachementArticleRepository;
        _mapper = mapper;
    }

    public async Task<GetOneRattachementArticleDto> Handle(
        GetOneRattachementArticleQuery request,
        CancellationToken cancellationToken)
    {
        var rattachementArticleId = new RattachementArticleId(request.RattachementArticleId);

        var rattachementArticle = await _rattachementArticleRepository
            .GetOneAsync(rattachementArticleId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOneRattachementArticleDto>(rattachementArticle);
    }
}
