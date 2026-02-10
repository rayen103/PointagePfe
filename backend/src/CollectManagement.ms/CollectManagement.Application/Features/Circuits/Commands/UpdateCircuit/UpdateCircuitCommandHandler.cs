using CollectManagement.Application.Interfaces.Repositories.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public class UpdateCircuitCommandHandler
    : IRequestHandler<UpdateCircuitCommand, UpdateCircuitResponse>
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IMapper _mapper;

    public UpdateCircuitCommandHandler(
        ICircuitRepository circuitRepository,
        IMapper mapper)
    {
        _circuitRepository = circuitRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCircuitResponse> Handle(UpdateCircuitCommand request, CancellationToken cancellationToken)
    {
        var circuitId = new CircuitId(request.CircuitId);

        var circuit = await _circuitRepository
            .GetOneAsync(circuitId, cancellationToken)
            .ConfigureAwait(false);

        circuit.Update(
            request.CodeCircuit,
            request.LibelleCircuit,
            request.Description,
            request.IsActive
        );

        await _circuitRepository
            .UpdateBulkAsync(circuit, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateCircuitResponse>(circuit);
    }
}
