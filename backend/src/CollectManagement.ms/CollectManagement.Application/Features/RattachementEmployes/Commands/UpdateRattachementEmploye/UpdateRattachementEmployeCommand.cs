namespace CollectManagement.Application.Features.RattachementEmployes.Commands.UpdateRattachementEmploye;

public record UpdateRattachementEmployeCommand(
    Ulid RattachementEmployeId,
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
    bool IsActive
) : IRequest<UpdateRattachementEmployeResponse>;
