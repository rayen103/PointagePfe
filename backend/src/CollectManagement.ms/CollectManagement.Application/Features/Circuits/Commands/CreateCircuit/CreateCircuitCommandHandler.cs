using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;

public class CreateCircuitCommandHandler
    : IRequestHandler<CreateCircuitCommand, CreateCircuitResponse>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly ICircuitPointCollecteRepository _circuitPointCollecteRepository;
    private readonly IMapper _mapper;

    public CreateCircuitCommandHandler(
        ICircuitRepository circuitRepository,
        IPointCollecteRepository pointCollecteRepository,
        ICircuitPointCollecteRepository circuitPointCollecteRepository,
        IMapper mapper)
    {
        _circuitRepository = circuitRepository;
        _pointCollecteRepository = pointCollecteRepository;
        _circuitPointCollecteRepository = circuitPointCollecteRepository;
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

        if (request.PointCollecteIds != null)
        {
            var requestedPointIds = new HashSet<PointCollecteId>();
            var orderedPointIds = new List<PointCollecteId>();
            foreach (var idStr in request.PointCollecteIds)
            {
                if (Ulid.TryParse(idStr, out var parsedUlid))
                {
                    var pid = new PointCollecteId(parsedUlid);
                    if (requestedPointIds.Add(pid))
                    {
                        orderedPointIds.Add(pid);
                    }
                }
            }

            var intermediatePoints = new List<PointCollecte>();
            foreach (var ptId in orderedPointIds)
            {
                var pt = await _pointCollecteRepository.GetOneAsync(ptId, cancellationToken).ConfigureAwait(false);
                if (pt != null)
                {
                    intermediatePoints.Add(pt);
                    pt.AssignCircuit(circuitId);
                    await _pointCollecteRepository.UpdateBulkAsync(pt, cancellationToken).ConfigureAwait(false);
                }
            }

            var newCircuitPoints = new List<CircuitPointCollecte>();
            int ordre = 0;

            if (!string.IsNullOrWhiteSpace(request.CodePCDepart))
            {
                var depPoint = await _pointCollecteRepository
                    .GetAsync(p => p.CodePointCollecte == request.CodePCDepart, cancellationToken)
                    .ConfigureAwait(false);
                if (depPoint != null)
                {
                    newCircuitPoints.Add(CircuitPointCollecte.Create(
                        new CircuitPointCollecteId(Ulid.NewUlid()),
                        circuitId,
                        depPoint.CodePointCollecte,
                        depPoint.LibellePointCollecte,
                        depPoint.Latitude,
                        depPoint.Longitude,
                        ordre++
                    ));
                }
            }

            foreach (var pt in intermediatePoints)
            {
                newCircuitPoints.Add(CircuitPointCollecte.Create(
                    new CircuitPointCollecteId(Ulid.NewUlid()),
                    circuitId,
                    pt.CodePointCollecte,
                    pt.LibellePointCollecte,
                    pt.Latitude,
                    pt.Longitude,
                    ordre++
                ));
            }

            if (!string.IsNullOrWhiteSpace(request.CodePCArrivee))
            {
                var arrPoint = await _pointCollecteRepository
                    .GetAsync(p => p.CodePointCollecte == request.CodePCArrivee, cancellationToken)
                    .ConfigureAwait(false);
                if (arrPoint != null)
                {
                    newCircuitPoints.Add(CircuitPointCollecte.Create(
                        new CircuitPointCollecteId(Ulid.NewUlid()),
                        circuitId,
                        arrPoint.CodePointCollecte,
                        arrPoint.LibellePointCollecte,
                        arrPoint.Latitude,
                        arrPoint.Longitude,
                        ordre++
                    ));
                }
            }

            if (newCircuitPoints.Count > 0)
            {
                await _circuitPointCollecteRepository
                    .AddRangeAsync(newCircuitPoints, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        return _mapper.Map<CreateCircuitResponse>(circuit);
    }
}
