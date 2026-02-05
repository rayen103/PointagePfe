namespace CollectManagement.Application.Features.Employes.Queries.GetPagedListEmploye;

public record GetPagedListEmployeDto(
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
