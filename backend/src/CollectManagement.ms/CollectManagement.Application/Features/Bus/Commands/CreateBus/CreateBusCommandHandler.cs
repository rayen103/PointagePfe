using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Bus.Commands.CreateBus;

public class CreateBusCommandHandler
    : IRequestHandler<CreateBusCommand, CreateBusResponse>
{
    private readonly IBusRepository _busRepository;
    private readonly IMapper _mapper;

    public CreateBusCommandHandler(
        IBusRepository busRepository,
        IMapper mapper)
    {
        _busRepository = busRepository;
        _mapper = mapper;
    }

    public async Task<CreateBusResponse> Handle(CreateBusCommand request, CancellationToken cancellationToken)
    {
        var busId = new BusId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        var bus = Domain.Bus.Bus.Create(
            busId,
            request.NumeroIMM,
            request.ModelBus,
            request.IMEI,
            request.Capacite,
            request.CodeCircuit,
            request.AppSagem,
            request.IsActive,
            request.Latitude,
            request.Longitude,
            societeId);

        await _busRepository
            .AddAsync(bus, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateBusResponse>(bus);
    }
}
