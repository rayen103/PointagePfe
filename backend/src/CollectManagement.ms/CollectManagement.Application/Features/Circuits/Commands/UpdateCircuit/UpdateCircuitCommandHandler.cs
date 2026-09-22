using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Application.Interfaces.Repositories.CircuitsPointsCollecte;
using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public class UpdateCircuitCommandHandler
    : IRequestHandler<UpdateCircuitCommand, UpdateCircuitResponse>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly ICircuitPointCollecteRepository _circuitPointCollecteRepository;
    private readonly IMapper _mapper;

    public UpdateCircuitCommandHandler(
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

            // 1. Unassign points that were previously assigned to this circuit but are no longer selected
            var existingAssignedPoints = await _pointCollecteRepository
                .GetManyAsync(p => p.CircuitId == circuitId, cancellationToken)
                .ConfigureAwait(false);

            if (existingAssignedPoints != null)
            {
                foreach (var pt in existingAssignedPoints)
                {
                    if (!requestedPointIds.Contains(pt.PointCollecteId))
                    {
                        pt.AssignCircuit(null);
                        await _pointCollecteRepository.UpdateBulkAsync(pt, cancellationToken).ConfigureAwait(false);
                    }
                }
            }

            // 2. Assign selected points to this circuit and retrieve in order
            var intermediatePoints = new List<PointCollecte>();
            foreach (var ptId in orderedPointIds)
            {
                var pt = await _pointCollecteRepository.GetOneAsync(ptId, cancellationToken).ConfigureAwait(false);
                if (pt != null)
                {
                    intermediatePoints.Add(pt);
                    if (pt.CircuitId != circuitId)
                    {
                        pt.AssignCircuit(circuitId);
                        await _pointCollecteRepository.UpdateBulkAsync(pt, cancellationToken).ConfigureAwait(false);
                    }
                }
            }

            // 3. Clear existing CircuitPointCollecte records for this circuit
            await _circuitPointCollecteRepository
                .DeleteAsync(c => c.CircuitId == circuitId, cancellationToken)
                .ConfigureAwait(false);

            // 4. Rebuild CircuitPointCollecte sequence (departure -> intermediate points -> arrival)
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

        return _mapper.Map<UpdateCircuitResponse>(circuit);
    }
}
