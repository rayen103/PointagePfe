using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Queries.GetByCircuit;

public class GetByCircuitQueryHandler
    : IRequestHandler<GetByCircuitQuery, GetByCircuitResponse>
{
    private readonly ICircuitPointCollecteRepository _repository;
    private readonly IMapper _mapper;

    public GetByCircuitQueryHandler(
        ICircuitPointCollecteRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByCircuitResponse> Handle(
        GetByCircuitQuery request,
        CancellationToken cancellationToken)
    {
        var circuitId = new CircuitId(request.CircuitId);

        var items = await _repository
            .GetByCircuitAsync(circuitId, cancellationToken)
            .ConfigureAwait(false);

        var dtos = _mapper.Map<IReadOnlyList<GetByCircuitDto>>(items);

        return new GetByCircuitResponse { Items = dtos };
    }
}
