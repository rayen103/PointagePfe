using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.Rattachements.Queries.GetOneRattachement;

public class GetOneRattachementQueryHandler
    : IRequestHandler<GetOneRattachementQuery, GetOneRattachementDto>
{
    private readonly IRattachementRepository _rattachementRepository;
    private readonly IMapper _mapper;

    public GetOneRattachementQueryHandler(
        IRattachementRepository rattachementRepository,
        IMapper mapper)
    {
        _rattachementRepository = rattachementRepository;
        _mapper = mapper;
    }

    public async Task<GetOneRattachementDto> Handle(GetOneRattachementQuery request, CancellationToken cancellationToken)
    {
        var rattachementId = new RattachementId(request.RattachementId);

        var rattachement = await _rattachementRepository
            .GetOneAsync(rattachementId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOneRattachementDto>(rattachement);
    }
}
