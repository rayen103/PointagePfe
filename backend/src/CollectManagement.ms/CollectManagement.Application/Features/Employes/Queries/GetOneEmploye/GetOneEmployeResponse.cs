namespace CollectManagement.Application.Features.Employes.Queries.GetOneEmploye;

public record GetOneEmployeResponse(
    string EmployeId,
    string Matricule,
    string? RFID,
    string Nom,
    string Prenom,
    string? CodeCircuit,
    string? CodePointCollecte,
    string? CodeBus,
    string? CodeShift,
    string? Adresse,
    string? CodeGouvernorat,
    string? CodeRegion,
    double? Latitude,
    double? Longitude,
    string SocieteId
);
