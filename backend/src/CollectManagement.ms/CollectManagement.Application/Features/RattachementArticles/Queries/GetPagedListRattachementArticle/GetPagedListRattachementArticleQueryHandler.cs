using CollectManagement.Application.Interfaces.Repositories.Rattachements;

namespace CollectManagement.Application.Features.RattachementArticles.Queries.GetPagedListRattachementArticle;

public class GetPagedListRattachementArticleQueryHandler
    : IRequestHandler<GetPagedListRattachementArticleQuery, GetPagedListRattachementArticleResponse>
{
    private readonly IRattachementArticleRepository _rattachementArticleRepository;
    private readonly IMapper _mapper;

    public GetPagedListRattachementArticleQueryHandler(
        IRattachementArticleRepository rattachementArticleRepository,
        IMapper mapper)
    {
        _rattachementArticleRepository = rattachementArticleRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListRattachementArticleResponse> Handle(
        GetPagedListRattachementArticleQuery request,
        CancellationToken cancellationToken)
    {
        var (rattachementArticles, totalCount) = await _rattachementArticleRepository
            .GetPagedListAsync(request.Search, request.Sort, request.Order, request.Page, request.Size, cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListRattachementArticleResponse
        {
            RattachementArticles = _mapper.Map<IReadOnlyList<GetPagedListRattachementArticleDto>>(rattachementArticles),
            TotalCount = totalCount
        };
    }
}
