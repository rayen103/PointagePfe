using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;

public class CreateCircuitCommandHandler
    : IRequestHandler<CreateCircuitCommand, CreateCircuitResponse>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IMapper _mapper;

    public CreateCircuitCommandHandler(
        ICircuitRepository circuitRepository,
        IMapper mapper)
    {
        _circuitRepository = circuitRepository;
        _mapper = mapper;
    }

    public async Task<CreateCircuitResponse> Handle(CreateCircuitCommand request, CancellationToken cancellationToken)
    {
        var circuitId = new CircuitId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        var circuit = Circuit.Create(
            circuitId,
            request.CodeCircuit,
            request.LibelleCircuit,
            request.Description,
            request.IsActive,
            societeId,
            request.Latitude,
            request.Longitude,
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
