using CollectManagement.Application.Interfaces.Repositories.Chantiers;
using CollectManagement.Domain.Chantiers.ValueObjects;

namespace CollectManagement.Application.Features.Chantiers.Queries.GetOneChantier;

public class GetOneChantierQueryHandler : IRequestHandler<GetOneChantierQuery, GetOneChantierDto>
{
    private readonly IChantierRepository _chantierRepository;
    private readonly IMapper _mapper;

    public GetOneChantierQueryHandler(IChantierRepository chantierRepository, IMapper mapper)
    {
        _chantierRepository = chantierRepository;
        _mapper = mapper;
    }

    public async Task<GetOneChantierDto> Handle(GetOneChantierQuery request, CancellationToken cancellationToken)
    {
        var chantierId = new ChantierId(request.ChantierId);
        var chantier = await _chantierRepository.GetOneAsync(chantierId, cancellationToken).ConfigureAwait(false);
        return _mapper.Map<GetOneChantierDto>(chantier);
    }
}
