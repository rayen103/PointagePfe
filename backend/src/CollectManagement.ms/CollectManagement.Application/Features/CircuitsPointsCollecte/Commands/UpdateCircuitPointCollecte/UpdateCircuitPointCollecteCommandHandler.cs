using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.UpdateCircuitPointCollecte;

public class UpdateCircuitPointCollecteCommandHandler
    : IRequestHandler<UpdateCircuitPointCollecteCommand, UpdateCircuitPointCollecteResponse>
{
    private readonly ICircuitPointCollecteRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCircuitPointCollecteCommandHandler(
        ICircuitPointCollecteRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateCircuitPointCollecteResponse> Handle(
        UpdateCircuitPointCollecteCommand request,
        CancellationToken cancellationToken)
    {
        var id = new CircuitPointCollecteId(request.CircuitPointCollecteId);

        var entity = await _repository
            .GetOneAsync(id, cancellationToken)
            .ConfigureAwait(false);

        entity.Update(
            request.CodePointCollecte,
            request.LibellePointCollecte,
            request.Latitude,
            request.Longitude,
            request.Ordre
        );

        await _repository
            .UpdateBulkAsync(entity, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateCircuitPointCollecteResponse>(entity);
    }
}
