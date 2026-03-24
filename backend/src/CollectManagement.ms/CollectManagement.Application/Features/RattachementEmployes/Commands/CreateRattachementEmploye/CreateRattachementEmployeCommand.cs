namespace CollectManagement.Application.Features.RattachementEmployes.Commands.CreateRattachementEmploye;

public record CreateRattachementEmployeCommand(
    Ulid RattachementId,
    string Matricule,
    string? NomPrenom,
    DateTime? DateDebut,
    TimeSpan? HeureDebut,
    DateTime? DateFin,
    TimeSpan? HeureFin,
    decimal? NombreHeure,
    decimal? Cout,
    decimal? CoutGlobal,
    string? TypeRattachement,
    bool IsActive,
    Ulid SocieteId
) : IRequest<CreateRattachementEmployeResponse>;
