namespace CollectManagement.Application.Features.Bus.Commands.CreateBus;

public record CreateBusCommand(
    string NumeroIMM,
    string? ModelBus,
    string? IMEI,
    int? Capacite,
    string? CodeCircuit,
    bool AppSagem,
    bool IsActive,
    double? Latitude,
    double? Longitude,
    Ulid SocieteId
) : IRequest<CreateBusResponse>;
