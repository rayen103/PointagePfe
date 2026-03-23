using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Domain.Bus.ValueObjects;

namespace CollectManagement.Application.Features.Bus.Commands.DeleteBus;

public class DeleteBusCommandHandler
    : IRequestHandler<DeleteBusCommand, Unit>
{
    private readonly IBusRepository _busRepository;

    public DeleteBusCommandHandler(IBusRepository busRepository)
    {
        _busRepository = busRepository;
    }

    public async Task<Unit> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
    {
        var busId = new BusId(request.BusId);

        await _busRepository
            .DeleteAsync(c => c.BusId == busId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
