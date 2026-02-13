using CollectManagement.Application.Interfaces.Repositories.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;

namespace CollectManagement.Application.Features.Equipes.Queries.GetOneEquipe;

public class GetOneEquipeQueryHandler
    : IRequestHandler<GetOneEquipeQuery, GetOneEquipeDto>
{
    private readonly IEquipeRepository _equipeRepository;
    private readonly IMapper _mapper;

    public GetOneEquipeQueryHandler(
        IEquipeRepository equipeRepository,
        IMapper mapper)
    {
        _equipeRepository = equipeRepository;
        _mapper = mapper;
    }

    public async Task<GetOneEquipeDto> Handle(GetOneEquipeQuery request, CancellationToken cancellationToken)
    {
        var equipeId = new EquipeId(request.EquipeId);

        var equipe = await _equipeRepository
            .GetOneAsync(equipeId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOneEquipeDto>(equipe);
    }
}
