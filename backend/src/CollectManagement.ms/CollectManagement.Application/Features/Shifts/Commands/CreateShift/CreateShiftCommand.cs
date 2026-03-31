namespace CollectManagement.Application.Features.Shifts.Commands.CreateShift;

public record CreateShiftCommand(
    string CodeShift,
    string? LibelleShift,
    string? JourSemaine,
    TimeSpan? HeureDebut,
    TimeSpan? HeureFin,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateShiftResponse>;
