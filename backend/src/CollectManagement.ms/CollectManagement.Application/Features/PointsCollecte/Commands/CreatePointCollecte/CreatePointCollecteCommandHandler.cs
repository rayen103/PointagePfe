using CollectManagement.Application.Interfaces.Repositories.PointsCollecte;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.PointsCollecte.Commands.CreatePointCollecte;

public class CreatePointCollecteCommandHandler
    : IRequestHandler<CreatePointCollecteCommand, CreatePointCollecteResponse>
{
    private readonly IPointCollecteRepository _pointCollecteRepository;
    private readonly IMapper _mapper;

    public CreatePointCollecteCommandHandler(
        IPointCollecteRepository pointCollecteRepository,
        IMapper mapper)
    {
        _pointCollecteRepository = pointCollecteRepository;
        _mapper = mapper;
    }

    public async Task<CreatePointCollecteResponse> Handle(CreatePointCollecteCommand request, CancellationToken cancellationToken)
    {
        var pointCollecteId = new PointCollecteId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);
        var circuitId = request.CircuitId != null ? new CircuitId(request.CircuitId.Value) : null;

        var pointCollecte = PointCollecte.Create(
            pointCollecteId,
            request.CodePointCollecte,
            request.LibellePointCollecte,
            request.Latitude,
            request.Longitude,
            request.CodeGouvernorat,
            request.CodeRegion,
            request.IsActive,
            societeId,
            circuitId
        );

        await _pointCollecteRepository
            .AddAsync(pointCollecte, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreatePointCollecteResponse>(pointCollecte);
    }
}
