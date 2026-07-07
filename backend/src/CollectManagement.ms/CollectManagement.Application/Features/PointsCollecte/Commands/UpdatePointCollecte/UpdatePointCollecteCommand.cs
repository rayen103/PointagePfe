namespace CollectManagement.Application.Features.PointsCollecte.Commands.UpdatePointCollecte;

public record UpdatePointCollecteCommand(
    Ulid PointCollecteId,
    string CodePointCollecte,
    string LibellePointCollecte,
    decimal? Latitude,
    decimal? Longitude,
    string? CodeGouvernorat,
    string? CodeRegion,
    bool IsActive,
    Ulid? CircuitId
) : IRequest<UpdatePointCollecteResponse>;
