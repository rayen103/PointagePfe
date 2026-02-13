namespace CollectManagement.Application.Features.PointsCollecte.Commands.CreatePointCollecte;

public record CreatePointCollecteCommand(
    string CodePointCollecte,
    string LibellePointCollecte,
    decimal? Latitude,
    decimal? Longitude,
    string? CodeGouvernorat,
    string? CodeRegion,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreatePointCollecteResponse>;
