using CollectManagement.Domain.Employes.Enums;

namespace CollectManagement.Application.Features.Employes.Commands.CreateEmploye;

public record CreateEmployeResponse(
    string EmployeId,
    string Matricule,
    string? RFID,
    string Nom,
    string Prenom,
    TypeEmploye TypeEmploye,
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
