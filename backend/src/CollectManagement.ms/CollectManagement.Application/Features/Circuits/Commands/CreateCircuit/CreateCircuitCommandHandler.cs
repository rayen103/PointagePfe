using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;

public class CreateCircuitCommandHandler
    : IRequestHandler<CreateCircuitCommand, CreateCircuitResponse>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly IMapper _mapper;

    public CreateCircuitCommandHandler(
        ICircuitRepository circuitRepository,
        IPointCollecteRepository pointCollecteRepository,
        IMapper mapper)
    {
        _circuitRepository = circuitRepository;
        _pointCollecteRepository = pointCollecteRepository;
        _mapper = mapper;
    }

    public async Task<CreateCircuitResponse> Handle(CreateCircuitCommand request, CancellationToken cancellationToken)
    {
        var circuitId = new CircuitId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        double? latitude = request.Latitude;
        double? longitude = request.Longitude;

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

        var circuit = Circuit.Create(
            circuitId,
            request.CodeCircuit,
            request.LibelleCircuit,
            request.Description,
            request.IsActive,
            societeId,
            latitude,
            longitude,
            request.CodePCDepart,
            request.CodePCArrivee,
            request.DistanceKm,
            request.DureeMinutes,
            request.Couleur
        );

        await _circuitRepository
            .AddAsync(circuit, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateCircuitResponse>(circuit);
    }
}
