namespace CollectManagement.Application.Features.Employes.Queries.GetOneEmploye;

public record GetOneEmployeResponse(
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
);
