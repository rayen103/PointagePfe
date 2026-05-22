namespace CollectManagement.Application.Features.Reseaux.Commands.UpdateReseau;

public record UpdateReseauCommand(
    Ulid ReseauId,
    string IpAddress,
    int Port,
    int? GmtPlus,
    decimal? Latitude,
    decimal? Longitude,
    decimal? Rayon,
    int? TimeToleranceMinute,
    bool IsActive
) : IRequest<UpdateReseauResponse>;
