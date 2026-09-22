using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public class UpdateCircuitCommandHandler
    : IRequestHandler<UpdateCircuitCommand, UpdateCircuitResponse>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly IMapper _mapper;

    public UpdateCircuitCommandHandler(
        ICircuitRepository circuitRepository,
        IPointCollecteRepository pointCollecteRepository,
        IMapper mapper)
    {
        _circuitRepository = circuitRepository;
        _pointCollecteRepository = pointCollecteRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCircuitResponse> Handle(UpdateCircuitCommand request, CancellationToken cancellationToken)
    {
        var circuitId = new CircuitId(request.CircuitId);

        var circuit = await _circuitRepository
            .GetOneAsync(circuitId, cancellationToken)
            .ConfigureAwait(false);

        double? latitude = request.Latitude ?? circuit.Latitude;
        double? longitude = request.Longitude ?? circuit.Longitude;

        if ((latitude == null || longitude == null) && !string.IsNullOrWhiteSpace(request.CodePCDepart))
        {
            var depPoint = await _pointCollecteRepository
                .GetAsync(p => p.CodePointCollecte == request.CodePCDepart, cancellationToken)
                .ConfigureAwait(false);
            if (depPoint != null && depPoint.Latitude.HasValue && depPoint.Longitude.HasValue)
            {
                latitude = (double?)depPoint.Latitude.Value;
                longitude = (double?)depPoint.Longitude.Value;
            }
        }

        circuit.Update(
            request.CodeCircuit,
            request.LibelleCircuit,
            request.Description,
            request.IsActive,
            latitude,
            longitude,
            request.CodePCDepart,
            request.CodePCArrivee,
            request.DistanceKm,
            request.DureeMinutes,
            request.Couleur
        );

        await _circuitRepository
            .UpdateBulkAsync(circuit, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateCircuitResponse>(circuit);
    }
}
