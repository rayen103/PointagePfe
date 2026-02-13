using CollectManagement.Domain.Common;
using CollectManagement.Domain.OrdresTravail.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.OrdresTravail;

public class OrdreTravail : AuditableEntity
{
    public OrdreTravailId OrdreTravailId { get; private set; }
    
    public string NumeroOrdreTravail { get; private set; }
    
    public string? NumeroChantier { get; private set; }
    
    public string? CodeClient { get; private set; }
    
    public string? NumeroBonCommande { get; private set; }
    
    public string? CodeEquipe { get; private set; }
    
    public string? EtatOT { get; private set; }
    
    public decimal? Montant { get; private set; }
    
    public DateTime? DateCreation { get; private set; }
    
    public string? NumeroConvention { get; private set; }
    
    public string? CodeVehicule { get; private set; }
    
    public string? Libelle { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    public SocieteId SocieteId { get; private set; }
    
    public Societe? Societe { get; private set; }

    private OrdreTravail(
        OrdreTravailId ordreTravailId,
        string numeroOrdreTravail,
        string? numeroChantier,
        string? codeClient,
        string? numeroBonCommande,
        string? codeEquipe,
        string? etatOT,
        decimal? montant,
        DateTime? dateCreation,
        string? numeroConvention,
        string? codeVehicule,
        string? libelle,
        bool isActive,
        SocieteId societeId)
    {
        OrdreTravailId = ordreTravailId;
        NumeroOrdreTravail = numeroOrdreTravail;
        NumeroChantier = numeroChantier;
        CodeClient = codeClient;
        NumeroBonCommande = numeroBonCommande;
        CodeEquipe = codeEquipe;
        EtatOT = etatOT;
        Montant = montant;
        DateCreation = dateCreation;
        NumeroConvention = numeroConvention;
        CodeVehicule = codeVehicule;
        Libelle = libelle;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static OrdreTravail Create(
        OrdreTravailId ordreTravailId,
        string numeroOrdreTravail,
        string? numeroChantier,
        string? codeClient,
        string? numeroBonCommande,
        string? codeEquipe,
        string? etatOT,
        decimal? montant,
        DateTime? dateCreation,
        string? numeroConvention,
        string? codeVehicule,
        string? libelle,
        bool isActive,
        SocieteId societeId)
    {
        return new OrdreTravail(
            ordreTravailId,
            numeroOrdreTravail,
            numeroChantier,
            codeClient,
            numeroBonCommande,
            codeEquipe,
            etatOT,
            montant,
            dateCreation,
            numeroConvention,
            codeVehicule,
            libelle,
            isActive,
            societeId);
    }

    public void Update(
        string numeroOrdreTravail,
        string? numeroChantier,
        string? codeClient,
        string? numeroBonCommande,
        string? codeEquipe,
        string? etatOT,
        decimal? montant,
        DateTime? dateCreation,
        string? numeroConvention,
        string? codeVehicule,
        string? libelle,
        bool isActive)
    {
        NumeroOrdreTravail = numeroOrdreTravail;
        NumeroChantier = numeroChantier;
        CodeClient = codeClient;
        NumeroBonCommande = numeroBonCommande;
        CodeEquipe = codeEquipe;
        EtatOT = etatOT;
        Montant = montant;
        DateCreation = dateCreation;
        NumeroConvention = numeroConvention;
        CodeVehicule = codeVehicule;
        Libelle = libelle;
        IsActive = isActive;
    }
    
    public static OrdreTravail QueryCreate(
        OrdreTravailId ordreTravailId,
        string numeroOrdreTravail,
        string? numeroChantier,
        string? codeClient,
        string? numeroBonCommande,
        string? codeEquipe,
        string? etatOT,
        decimal? montant,
        DateTime? dateCreation,
        string? numeroConvention,
        string? codeVehicule,
        string? libelle,
        bool isActive,
        SocieteId societeId)
    {
        return new OrdreTravail(
            ordreTravailId,
            numeroOrdreTravail,
            numeroChantier,
            codeClient,
            numeroBonCommande,
            codeEquipe,
            etatOT,
            montant,
            dateCreation,
            numeroConvention,
            codeVehicule,
            libelle,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private OrdreTravail() { }
#pragma warning restore CS8618
}
