using CollectManagement.Application.Interfaces.Repositories.Shifts;
using CollectManagement.Domain.Shifts.ValueObjects;

namespace CollectManagement.Application.Features.Shifts.Commands.DeleteShift;

public class DeleteShiftCommandHandler
    : IRequestHandler<DeleteShiftCommand, Unit>
{
    private readonly IShiftRepository _shiftRepository;

    public DeleteShiftCommandHandler(IShiftRepository shiftRepository)
    {
        _shiftRepository = shiftRepository;
    }

    public async Task<Unit> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
    {
        var shiftId = new ShiftId(request.ShiftId);

        await _shiftRepository
            .DeleteAsync(c => c.ShiftId == shiftId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
