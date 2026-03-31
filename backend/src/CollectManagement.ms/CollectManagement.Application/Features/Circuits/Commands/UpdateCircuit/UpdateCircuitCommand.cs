namespace CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;

public record UpdateCircuitCommand(
    Ulid CircuitId,
    string CodeCircuit,
    string? LibelleCircuit,
    string? Description,
    double? Latitude,
    double? Longitude,
    bool IsActive,
    string? CodePCDepart,
    string? CodePCArrivee,
    decimal? DistanceKm,
    int? DureeMinutes,
    string? Couleur
) : IRequest<UpdateCircuitResponse>;
