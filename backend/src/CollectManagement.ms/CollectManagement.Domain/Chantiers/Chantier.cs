using CollectManagement.Domain.Chantiers.ValueObjects;
using CollectManagement.Domain.Common;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Chantiers;

public class Chantier : AuditableEntity
{
    public ChantierId ChantierId { get; private set; }

    public string NumeroChantier { get; private set; }

    public string? LibelleChantier { get; private set; }

    public string? CodeClient { get; private set; }

    public string? Adresse { get; private set; }

    public decimal? MontantHT { get; private set; }

    public decimal? MontantTTC { get; private set; }

    public string? Nature { get; private set; }

    public string? Responsable { get; private set; }

    public DateTime? DateDebut { get; private set; }

    public DateTime? DateFin { get; private set; }

    public string? Status { get; private set; }

    public bool IsActive { get; private set; } = true;

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Chantier(
        ChantierId chantierId,
        string numeroChantier,
        string? libelleChantier,
        string? codeClient,
        string? adresse,
        decimal? montantHT,
        decimal? montantTTC,
        string? nature,
        string? responsable,
        DateTime? dateDebut,
        DateTime? dateFin,
        string? status,
        bool isActive,
        SocieteId societeId)
    {
        ChantierId = chantierId;
        NumeroChantier = numeroChantier;
        LibelleChantier = libelleChantier;
        CodeClient = codeClient;
        Adresse = adresse;
        MontantHT = montantHT;
        MontantTTC = montantTTC;
        Nature = nature;
        Responsable = responsable;
        DateDebut = dateDebut;
        DateFin = dateFin;
        Status = status;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Chantier Create(
        ChantierId chantierId,
        string numeroChantier,
        string? libelleChantier,
        string? codeClient,
        string? adresse,
        decimal? montantHT,
        decimal? montantTTC,
        string? nature,
        string? responsable,
        DateTime? dateDebut,
        DateTime? dateFin,
        string? status,
        bool isActive,
        SocieteId societeId)
    {
        return new Chantier(
            chantierId,
            numeroChantier,
            libelleChantier,
            codeClient,
            adresse,
            montantHT,
            montantTTC,
            nature,
            responsable,
            dateDebut,
            dateFin,
            status,
            isActive,
            societeId);
    }

    public void Update(
        string numeroChantier,
        string? libelleChantier,
        string? codeClient,
        string? adresse,
        decimal? montantHT,
        decimal? montantTTC,
        string? nature,
        string? responsable,
        DateTime? dateDebut,
        DateTime? dateFin,
        string? status,
        bool isActive)
    {
        NumeroChantier = numeroChantier;
        LibelleChantier = libelleChantier;
        CodeClient = codeClient;
        Adresse = adresse;
        MontantHT = montantHT;
        MontantTTC = montantTTC;
        Nature = nature;
        Responsable = responsable;
        DateDebut = dateDebut;
        DateFin = dateFin;
        Status = status;
        IsActive = isActive;
    }

    public static Chantier QueryCreate(
        ChantierId chantierId,
        string numeroChantier,
        string? libelleChantier,
        string? codeClient,
        string? adresse,
        decimal? montantHT,
        decimal? montantTTC,
        string? nature,
        string? responsable,
        DateTime? dateDebut,
        DateTime? dateFin,
        string? status,
        bool isActive,
        SocieteId societeId)
    {
        return new Chantier(
            chantierId,
            numeroChantier,
            libelleChantier,
            codeClient,
            adresse,
            montantHT,
            montantTTC,
            nature,
            responsable,
            dateDebut,
            dateFin,
            status,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Chantier() { }
#pragma warning restore CS8618
}
