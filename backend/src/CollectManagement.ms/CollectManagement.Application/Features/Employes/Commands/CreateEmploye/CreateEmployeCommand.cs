namespace CollectManagement.Application.Features.Employes.Commands.CreateEmploye;

public record CreateEmployeCommand(
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
) : IRequest<CreateEmployeResponse>;
