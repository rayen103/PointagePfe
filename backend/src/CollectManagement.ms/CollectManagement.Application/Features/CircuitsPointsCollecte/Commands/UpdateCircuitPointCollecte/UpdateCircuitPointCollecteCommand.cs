namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.UpdateCircuitPointCollecte;

public record UpdateCircuitPointCollecteCommand(
    Ulid CircuitPointCollecteId,
    string CodePointCollecte,
    string? LibellePointCollecte,
    decimal? Latitude,
    decimal? Longitude,
    int? Ordre
) : IRequest<UpdateCircuitPointCollecteResponse>;
