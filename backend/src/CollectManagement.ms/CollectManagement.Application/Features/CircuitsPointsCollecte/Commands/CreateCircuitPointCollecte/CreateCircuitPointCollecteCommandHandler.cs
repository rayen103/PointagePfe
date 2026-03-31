using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.CreateCircuitPointCollecte;

public class CreateCircuitPointCollecteCommandHandler
    : IRequestHandler<CreateCircuitPointCollecteCommand, CreateCircuitPointCollecteResponse>
{
    private readonly ICircuitPointCollecteRepository _repository;
    private readonly IMapper _mapper;

    public CreateCircuitPointCollecteCommandHandler(
        ICircuitPointCollecteRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateCircuitPointCollecteResponse> Handle(
        CreateCircuitPointCollecteCommand request,
        CancellationToken cancellationToken)
    {
        var id = new CircuitPointCollecteId(Ulid.NewUlid());
        var circuitId = new CircuitId(request.CircuitId);

        var entity = CircuitPointCollecte.Create(
            id,
            circuitId,
            request.CodePointCollecte,
            request.LibellePointCollecte,
            request.Latitude,
            request.Longitude,
            request.Ordre
        );

        await _repository
            .AddAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateCircuitPointCollecteResponse>(entity);
    }
}
