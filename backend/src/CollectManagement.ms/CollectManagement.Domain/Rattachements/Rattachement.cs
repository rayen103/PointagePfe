using CollectManagement.Domain.Common;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Rattachements;

public class Rattachement : AuditableEntity
{
    public RattachementId RattachementId { get; private set; }
    
    public string NumeroRattachement { get; private set; }
    
    public int? Exercice { get; private set; }
    
    public DateTime DateRattachement { get; private set; }
    
    public string? NumeroChantier { get; private set; }
    
    public string? CodeClient { get; private set; }
    
    public bool IsInternal { get; private set; }
    
    public decimal? Cout { get; private set; }
    
    public string? Type { get; private set; }
    
    public string? Nature { get; private set; }
    
    public string? Responsable { get; private set; }
    
    public TimeSpan? HeureDebut { get; private set; }
    
    public TimeSpan? HeureFin { get; private set; }
    
    public string? Emplacement { get; private set; }
    
    public string? Reference { get; private set; }
    
    public string? Status { get; private set; }
    
    public DateTime? DateCloture { get; private set; }
    
    public string? Remarque { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    public SocieteId SocieteId { get; private set; }
    
    public Societe? Societe { get; private set; }

    private Rattachement(
        RattachementId rattachementId,
        string numeroRattachement,
        int? exercice,
        DateTime dateRattachement,
        string? numeroChantier,
        string? codeClient,
        bool isInternal,
        decimal? cout,
        string? type,
        string? nature,
        string? responsable,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        string? emplacement,
        string? reference,
        string? status,
        DateTime? dateCloture,
        string? remarque,
        bool isActive,
        SocieteId societeId)
    {
        RattachementId = rattachementId;
        NumeroRattachement = numeroRattachement;
        Exercice = exercice;
        DateRattachement = dateRattachement;
        NumeroChantier = numeroChantier;
        CodeClient = codeClient;
        IsInternal = isInternal;
        Cout = cout;
        Type = type;
        Nature = nature;
        Responsable = responsable;
        HeureDebut = heureDebut;
        HeureFin = heureFin;
        Emplacement = emplacement;
        Reference = reference;
        Status = status;
        DateCloture = dateCloture;
        Remarque = remarque;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Rattachement Create(
        RattachementId rattachementId,
        string numeroRattachement,
        int? exercice,
        DateTime dateRattachement,
        string? numeroChantier,
        string? codeClient,
        bool isInternal,
        decimal? cout,
        string? type,
        string? nature,
        string? responsable,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        string? emplacement,
        string? reference,
        string? status,
        DateTime? dateCloture,
        string? remarque,
        bool isActive,
        SocieteId societeId)
    {
        return new Rattachement(
            rattachementId,
            numeroRattachement,
            exercice,
            dateRattachement,
            numeroChantier,
            codeClient,
            isInternal,
            cout,
            type,
            nature,
            responsable,
            heureDebut,
            heureFin,
            emplacement,
            reference,
            status,
            dateCloture,
            remarque,
            isActive,
            societeId);
    }

    public void Update(
        string numeroRattachement,
        int? exercice,
        DateTime dateRattachement,
        string? numeroChantier,
        string? codeClient,
        bool isInternal,
        decimal? cout,
        string? type,
        string? nature,
        string? responsable,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        string? emplacement,
        string? reference,
        string? status,
        DateTime? dateCloture,
        string? remarque,
        bool isActive)
    {
        NumeroRattachement = numeroRattachement;
        Exercice = exercice;
        DateRattachement = dateRattachement;
        NumeroChantier = numeroChantier;
        CodeClient = codeClient;
        IsInternal = isInternal;
        Cout = cout;
        Type = type;
        Nature = nature;
        Responsable = responsable;
        HeureDebut = heureDebut;
        HeureFin = heureFin;
        Emplacement = emplacement;
        Reference = reference;
        Status = status;
        DateCloture = dateCloture;
        Remarque = remarque;
        IsActive = isActive;
    }
    
    public static Rattachement QueryCreate(
        RattachementId rattachementId,
        string numeroRattachement,
        int? exercice,
        DateTime dateRattachement,
        string? numeroChantier,
        string? codeClient,
        bool isInternal,
        decimal? cout,
        string? type,
        string? nature,
        string? responsable,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        string? emplacement,
        string? reference,
        string? status,
        DateTime? dateCloture,
        string? remarque,
        bool isActive,
        SocieteId societeId)
    {
        return new Rattachement(
            rattachementId,
            numeroRattachement,
            exercice,
            dateRattachement,
            numeroChantier,
            codeClient,
            isInternal,
            cout,
            type,
            nature,
            responsable,
            heureDebut,
            heureFin,
            emplacement,
            reference,
            status,
            dateCloture,
            remarque,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Rattachement() { }
#pragma warning restore CS8618
}
