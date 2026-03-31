namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.CreateCircuitPointCollecte;

public record CreateCircuitPointCollecteCommand(
    Ulid CircuitId,
    string CodePointCollecte,
    string? LibellePointCollecte,
    decimal? Latitude,
    decimal? Longitude,
    int? Ordre
) : IRequest<CreateCircuitPointCollecteResponse>;
