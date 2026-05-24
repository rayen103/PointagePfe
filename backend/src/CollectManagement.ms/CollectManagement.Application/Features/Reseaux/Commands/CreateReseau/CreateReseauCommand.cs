namespace CollectManagement.Application.Features.Reseaux.Commands.CreateReseau;

public record CreateReseauCommand(
    string IpAddress,
    int Port,
    int? GmtPlus,
    decimal? Latitude,
    decimal? Longitude,
    decimal? Rayon,
    int? TimeToleranceMinute,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateReseauResponse>;
