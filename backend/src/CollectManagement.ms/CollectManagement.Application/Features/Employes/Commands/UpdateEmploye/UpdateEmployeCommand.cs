namespace CollectManagement.Application.Features.Employes.Commands.UpdateEmploye;

public record UpdateEmployeCommand(
    string EmployeId,
    string Matricule,
    string? RFID,
    string Nom,
    string Prenom,
    string? CodeCircuit,
    string? CodePointCollecte,
    string? CodeShift,
    string? Adresse,
    string? CodeGouvernorat,
    string? CodeRegion,
    string SocieteId
) : IRequest<Unit>;
