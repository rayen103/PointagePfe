namespace CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;

public record CreateCircuitCommand(
    string CodeCircuit,
    string? LibelleCircuit,
    string? Description,
    double? Latitude,
    double? Longitude,
    bool IsActive,
    Ulid SocieteId,
    string? CodePCDepart,
    string? CodePCArrivee,
    decimal? DistanceKm,
    int? DureeMinutes,
    string? Couleur
) : IRequest<CreateCircuitResponse>;
