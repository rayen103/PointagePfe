using CollectManagement.Application.Interfaces.Repositories.Reseaux;

namespace CollectManagement.Application.Features.Reseaux.Queries.GetPagedListReseau;

public class GetPagedListReseauQueryHandler : IRequestHandler<GetPagedListReseauQuery, GetPagedListReseauResponse>
{
    private readonly IReseauRepository _reseauRepository;
    private readonly IMapper _mapper;

    public GetPagedListReseauQueryHandler(IReseauRepository reseauRepository, IMapper mapper)
    {
        _reseauRepository = reseauRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListReseauResponse> Handle(GetPagedListReseauQuery request, CancellationToken cancellationToken)
    {
        var (reseaux, totalCount) = await _reseauRepository.GetPagedListAsync(request.Search, request.Sort, request.Order, request.Page, request.Size, request.SocieteId, cancellationToken).ConfigureAwait(false);
        return new GetPagedListReseauResponse(_mapper.Map<IReadOnlyList<GetPagedListReseauDto>>(reseaux), totalCount);
    }
}
