namespace CollectManagement.Application.Features.Bus.Commands.UpdateBus;

public record UpdateBusCommand(
    Ulid BusId,
    string NumeroIMM,
    string? ModelBus,
    string? IMEI,
    int? Capacite,
    string? CodeCircuit,
    string? CodeChauffeur,
    bool AppSagem,
    bool IsActive,
    double? Latitude,
    double? Longitude
) : IRequest<UpdateBusResponse>;
