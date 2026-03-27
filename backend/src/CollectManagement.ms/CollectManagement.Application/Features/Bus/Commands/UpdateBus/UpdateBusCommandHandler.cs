using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Domain.Bus.ValueObjects;

namespace CollectManagement.Application.Features.Bus.Commands.UpdateBus;

public class UpdateBusCommandHandler
    : IRequestHandler<UpdateBusCommand, UpdateBusResponse>
{
    private readonly IBusRepository _busRepository;
    private readonly IMapper _mapper;

    public UpdateBusCommandHandler(
        IBusRepository busRepository,
        IMapper mapper)
    {
        _busRepository = busRepository;
        _mapper = mapper;
    }

    public async Task<UpdateBusResponse> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
    {
        var busId = new BusId(request.BusId);

        var bus = await _busRepository
            .GetOneAsync(busId, cancellationToken)
            .ConfigureAwait(false);

        bus.Update(
            request.NumeroIMM,
            request.ModelBus,
            request.IMEI,
            request.Capacite,
            request.CodeCircuit,
            request.AppSagem,
            request.IsActive,
            request.Latitude,
            request.Longitude
        );

        await _busRepository
            .UpdateBulkAsync(bus, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateBusResponse>(bus);
    }
}
