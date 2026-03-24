using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.RattachementEmployes.Queries.GetOneRattachementEmploye;

public class GetOneRattachementEmployeQueryHandler
    : IRequestHandler<GetOneRattachementEmployeQuery, GetOneRattachementEmployeDto>
{
    private readonly IRattachementEmployeRepository _rattachementEmployeRepository;
    private readonly IMapper _mapper;

    public GetOneRattachementEmployeQueryHandler(
        IRattachementEmployeRepository rattachementEmployeRepository,
        IMapper mapper)
    {
        _rattachementEmployeRepository = rattachementEmployeRepository;
        _mapper = mapper;
    }

    public async Task<GetOneRattachementEmployeDto> Handle(
        GetOneRattachementEmployeQuery request,
        CancellationToken cancellationToken)
    {
        var rattachementEmployeId = new RattachementEmployeId(request.RattachementEmployeId);

        var rattachementEmploye = await _rattachementEmployeRepository
            .GetOneAsync(rattachementEmployeId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOneRattachementEmployeDto>(rattachementEmploye);
    }
}
