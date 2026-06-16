using BusEntity = CollectManagement.Domain.Bus.Bus;
using CollectManagement.Domain.Bus;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Chauffeurs.ValueObjects;
using CollectManagement.Domain.Common;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Chauffeurs;

public class Chauffeur : AuditableEntity
{
    public ChauffeurId ChauffeurId { get; private set; }

    public string CodeChauffeur { get; private set; }

    public string Nom { get; private set; }

    public string? Prenom { get; private set; }

    public string? CIN { get; private set; }

    public string? RFIDChauffeur { get; private set; }

    public bool Externe { get; private set; }

    public bool IsActive { get; private set; } = true;

    public BusId? BusId { get; private set; }

    public BusEntity? Bus { get; private set; }

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Chauffeur(
        ChauffeurId chauffeurId,
        string codeChauffeur,
        string nom,
        string? prenom,
        string? cin,
        string? rfidChauffeur,
        bool externe,
        bool isActive,
        SocieteId societeId,
        BusId? busId = null)
    {
        ChauffeurId = chauffeurId;
        CodeChauffeur = codeChauffeur;
        Nom = nom;
        Prenom = prenom;
        CIN = cin;
        RFIDChauffeur = rfidChauffeur;
        Externe = externe;
        IsActive = isActive;
        SocieteId = societeId;
        BusId = busId;
    }

    public static Chauffeur Create(
        ChauffeurId chauffeurId,
        string codeChauffeur,
        string nom,
        string? prenom,
        string? cin,
        string? rfidChauffeur,
        bool externe,
        bool isActive,
        SocieteId societeId,
        BusId? busId = null)
    {
        return new Chauffeur(
            chauffeurId,
            codeChauffeur,
            nom,
            prenom,
            cin,
            rfidChauffeur,
            externe,
            isActive,
            societeId,
            busId);
    }

    public void Update(
        string codeChauffeur,
        string nom,
        string? prenom,
        string? cin,
        string? rfidChauffeur,
        bool externe,
        bool isActive,
        BusId? busId = null)
    {
        CodeChauffeur = codeChauffeur;
        Nom = nom;
        Prenom = prenom;
        CIN = cin;
        RFIDChauffeur = rfidChauffeur;
        Externe = externe;
        IsActive = isActive;
        BusId = busId;
    }

    public static Chauffeur QueryCreate(
        ChauffeurId chauffeurId,
        string codeChauffeur,
        string nom,
        string? prenom,
        string? cin,
        string? rfidChauffeur,
        bool externe,
        bool isActive,
        SocieteId societeId,
        BusId? busId = null)
    {
        return new Chauffeur(
            chauffeurId,
            codeChauffeur,
            nom,
            prenom,
            cin,
            rfidChauffeur,
            externe,
            isActive,
            societeId,
            busId);
    }

#pragma warning disable CS8618
    private Chauffeur() { }
#pragma warning restore CS8618
}
