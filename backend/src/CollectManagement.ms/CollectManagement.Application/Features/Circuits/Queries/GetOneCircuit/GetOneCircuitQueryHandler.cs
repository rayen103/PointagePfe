using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Queries.GetOneCircuit;

public class GetOneCircuitQueryHandler
    : IRequestHandler<GetOneCircuitQuery, GetOneCircuitDto>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IMapper _mapper;

    public GetOneCircuitQueryHandler(
        ICircuitRepository circuitRepository,
        IMapper mapper)
    {
        _circuitRepository = circuitRepository;
        _mapper = mapper;
    }

    public async Task<GetOneCircuitDto> Handle(GetOneCircuitQuery request, CancellationToken cancellationToken)
    {
        var circuitId = new CircuitId(request.CircuitId);

        var circuit = await _circuitRepository
            .GetOneAsync(circuitId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOneCircuitDto>(circuit);
    }
}
