using CollectManagement.Domain.Common;
using CollectManagement.Domain.Employes.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Employes;

public class Employe : AuditableEntity
{
    public EmployeId EmployeId { get; private set; }
    
    public string Matricule { get; private set; }
    
    public string? RFID { get; private set; }
    
    public string Nom { get; private set; }
    
    public string Prenom { get; private set; }
    
    public string? CodeCircuit { get; private set; }
    
    public string? CodePointCollecte { get; private set; }
    
    public string? CodeBus { get; private set; }
    
    public string? CodeShift { get; private set; }
    
    public string? Adresse { get; private set; }
    
    public string? CodeGouvernorat { get; private set; }
    
    public string? CodeRegion { get; private set; }
    
    public double? Latitude { get; private set; }
    
    public double? Longitude { get; private set; }
    
    public SocieteId SocieteId { get; private set; }
    
    public Societe? Societe { get; private set; }

    private Employe(
        EmployeId employeId,
        string matricule,
        string? rfid,
        string nom,
        string prenom,
        string? codeCircuit,
        string? codePointCollecte,
        string? codeBus,
        string? codeShift,
        string? adresse,
        string? codeGouvernorat,
        string? codeRegion,
        double? latitude,
        double? longitude,
        SocieteId societeId)
    {
        EmployeId = employeId;
        Matricule = matricule;
        RFID = rfid;
        Nom = nom;
        Prenom = prenom;
        CodeCircuit = codeCircuit;
        CodePointCollecte = codePointCollecte;
        CodeBus = codeBus;
        CodeShift = codeShift;
        Adresse = adresse;
        CodeGouvernorat = codeGouvernorat;
        CodeRegion = codeRegion;
        Latitude = latitude;
        Longitude = longitude;
        SocieteId = societeId;
    }

    public static Employe Create(
        EmployeId employeId,
        string matricule,
        string? rfid,
        string nom,
        string prenom,
        string? codeCircuit,
        string? codePointCollecte,
        string? codeBus,
        string? codeShift,
        string? adresse,
        string? codeGouvernorat,
        string? codeRegion,
        double? latitude,
        double? longitude,
        SocieteId societeId)
    {
        return new Employe(
            employeId,
            matricule,
            rfid,
            nom,
            prenom,
            codeCircuit,
            codePointCollecte,
            codeBus,
            codeShift,
            adresse,
            codeGouvernorat,
            codeRegion,
            latitude,
            longitude,
            societeId);
    }

    public void Update(
        string matricule,
        string? rfid,
        string nom,
        string prenom,
        string? codeCircuit,
        string? codePointCollecte,
        string? codeBus,
        string? codeShift,
        string? adresse,
        string? codeGouvernorat,
        string? codeRegion,
        double? latitude,
        double? longitude,
        SocieteId societeId)
    {
        Matricule = matricule;
        RFID = rfid;
        Nom = nom;
        Prenom = prenom;
        CodeCircuit = codeCircuit;
        CodePointCollecte = codePointCollecte;
        CodeBus = codeBus;
        CodeShift = codeShift;
        Adresse = adresse;
        CodeGouvernorat = codeGouvernorat;
        CodeRegion = codeRegion;
        Latitude = latitude;
        Longitude = longitude;
        SocieteId = societeId;
    }

    public static Employe QueryCreate(
        EmployeId employeId,
        string matricule,
        string? rfid,
        string nom,
        string prenom,
        string? codeCircuit,
        string? codePointCollecte,
        string? codeBus,
        string? codeShift,
        string? adresse,
        string? codeGouvernorat,
        string? codeRegion,
        double? latitude,
        double? longitude,
        SocieteId societeId)
    {
        return new Employe(
            employeId,
            matricule,
            rfid,
            nom,
            prenom,
            codeCircuit,
            codePointCollecte,
            codeBus,
            codeShift,
            adresse,
            codeGouvernorat,
            codeRegion,
            latitude,
            longitude,
            societeId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Employe() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
