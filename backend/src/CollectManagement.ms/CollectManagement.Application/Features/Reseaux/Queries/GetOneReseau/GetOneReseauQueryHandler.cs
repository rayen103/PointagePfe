using CollectManagement.Application.Interfaces.Repositories.Reseaux;
using CollectManagement.Domain.Reseaux.ValueObjects;

namespace CollectManagement.Application.Features.Reseaux.Queries.GetOneReseau;

public class GetOneReseauQueryHandler : IRequestHandler<GetOneReseauQuery, GetOneReseauDto>
{
    private readonly IReseauRepository _reseauRepository;
    private readonly IMapper _mapper;

    public GetOneReseauQueryHandler(IReseauRepository reseauRepository, IMapper mapper)
    {
        _reseauRepository = reseauRepository;
        _mapper = mapper;
    }

    public async Task<GetOneReseauDto> Handle(GetOneReseauQuery request, CancellationToken cancellationToken)
    {
        var reseau = await _reseauRepository.GetOneAsync(new ReseauId(request.ReseauId), cancellationToken).ConfigureAwait(false);
        return _mapper.Map<GetOneReseauDto>(reseau);
    }
}
