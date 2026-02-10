using CollectManagement.Application.Interfaces.Repositories.Equipes;

namespace CollectManagement.Application.Features.Equipes.Queries.GetPagedListEquipe;

public class GetPagedListEquipeQueryHandler
    : IRequestHandler<GetPagedListEquipeQuery, GetPagedListEquipeResponse>
{
    private readonly IEquipeRepository _equipeRepository;
    private readonly IMapper _mapper;

    public GetPagedListEquipeQueryHandler(
        IEquipeRepository equipeRepository,
        IMapper mapper)
    {
        _equipeRepository = equipeRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListEquipeResponse> Handle(GetPagedListEquipeQuery request, CancellationToken cancellationToken)
    {
        var (equipes, totalCount) = await _equipeRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken)
            .ConfigureAwait(false);

        return new GetPagedListEquipeResponse
        {
            Equipes = _mapper.Map<IReadOnlyList<GetPagedListEquipeDto>>(equipes),
            TotalCount = totalCount
        };
    }
}
