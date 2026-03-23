namespace CollectManagement.Application.Features.Bus.Commands.UpdateBus;

public record UpdateBusCommand(
    Ulid BusId,
    string NumeroIMM,
    string? ModelBus,
    string? IMEI,
    int? Capacite,
    string? CodeCircuit,
    bool AppSagem,
    bool IsActive
) : IRequest<UpdateBusResponse>;
