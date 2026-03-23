using CollectManagement.Application.Interfaces.Repositories.Shifts;
using CollectManagement.Domain.Shifts.ValueObjects;

namespace CollectManagement.Application.Features.Shifts.Commands.UpdateShift;

public class UpdateShiftCommandHandler
    : IRequestHandler<UpdateShiftCommand, UpdateShiftResponse>
{
    private readonly IShiftRepository _shiftRepository;
    private readonly IMapper _mapper;

    public UpdateShiftCommandHandler(
        IShiftRepository shiftRepository,
        IMapper mapper)
    {
        _shiftRepository = shiftRepository;
        _mapper = mapper;
    }

    public async Task<UpdateShiftResponse> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
    {
        var shiftId = new ShiftId(request.ShiftId);

        var shift = await _shiftRepository
            .GetOneAsync(shiftId, cancellationToken)
            .ConfigureAwait(false);

        shift.Update(
            request.CodeShift,
            request.LibelleShift,
            request.JourSemaine,
            request.HeureDebut,
            request.HeureFin,
            request.IsActive
        );

        await _shiftRepository
            .UpdateBulkAsync(shift, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateShiftResponse>(shift);
    }
}
