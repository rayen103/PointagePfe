namespace CollectManagement.Application.Features.Shifts.Commands.UpdateShift;

public record UpdateShiftCommand(
    Ulid ShiftId,
    string CodeShift,
    string? LibelleShift,
    string? JourSemaine,
    TimeSpan? HeureDebut,
    TimeSpan? HeureFin,
    bool IsActive
) : IRequest<UpdateShiftResponse>;
