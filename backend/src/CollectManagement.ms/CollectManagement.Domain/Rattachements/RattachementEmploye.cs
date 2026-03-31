using CollectManagement.Domain.Common;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Rattachements;

public class RattachementEmploye : AuditableEntity
{
    public RattachementEmployeId RattachementEmployeId { get; private set; }

    public RattachementId RattachementId { get; private set; }

    public string Matricule { get; private set; }

    public string? NomPrenom { get; private set; }

    public DateTime? DateDebut { get; private set; }

    public TimeSpan? HeureDebut { get; private set; }

    public DateTime? DateFin { get; private set; }

    public TimeSpan? HeureFin { get; private set; }

    public decimal? NombreHeure { get; private set; }

    public decimal? Cout { get; private set; }

    public decimal? CoutGlobal { get; private set; }

    public string? TypeRattachement { get; private set; }

    public bool IsActive { get; private set; } = true;

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    public Rattachement? Rattachement { get; private set; }

    private RattachementEmploye(
        RattachementEmployeId rattachementEmployeId,
        RattachementId rattachementId,
        string matricule,
        string? nomPrenom,
        DateTime? dateDebut,
        TimeSpan? heureDebut,
        DateTime? dateFin,
        TimeSpan? heureFin,
        decimal? nombreHeure,
        decimal? cout,
        decimal? coutGlobal,
        string? typeRattachement,
        bool isActive,
        SocieteId societeId)
    {
        RattachementEmployeId = rattachementEmployeId;
        RattachementId = rattachementId;
        Matricule = matricule;
        NomPrenom = nomPrenom;
        DateDebut = dateDebut;
        HeureDebut = heureDebut;
        DateFin = dateFin;
        HeureFin = heureFin;
        NombreHeure = nombreHeure;
        Cout = cout;
        CoutGlobal = coutGlobal;
        TypeRattachement = typeRattachement;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static RattachementEmploye Create(
        RattachementEmployeId rattachementEmployeId,
        RattachementId rattachementId,
        string matricule,
        string? nomPrenom,
        DateTime? dateDebut,
        TimeSpan? heureDebut,
        DateTime? dateFin,
        TimeSpan? heureFin,
        decimal? nombreHeure,
        decimal? cout,
        decimal? coutGlobal,
        string? typeRattachement,
        bool isActive,
        SocieteId societeId)
    {
        return new RattachementEmploye(
            rattachementEmployeId,
            rattachementId,
            matricule,
            nomPrenom,
            dateDebut,
            heureDebut,
            dateFin,
            heureFin,
            nombreHeure,
            cout,
            coutGlobal,
            typeRattachement,
            isActive,
            societeId);
    }

    public void Update(
        RattachementId rattachementId,
        string matricule,
        string? nomPrenom,
        DateTime? dateDebut,
        TimeSpan? heureDebut,
        DateTime? dateFin,
        TimeSpan? heureFin,
        decimal? nombreHeure,
        decimal? cout,
        decimal? coutGlobal,
        string? typeRattachement,
        bool isActive)
    {
        RattachementId = rattachementId;
        Matricule = matricule;
        NomPrenom = nomPrenom;
        DateDebut = dateDebut;
        HeureDebut = heureDebut;
        DateFin = dateFin;
        HeureFin = heureFin;
        NombreHeure = nombreHeure;
        Cout = cout;
        CoutGlobal = coutGlobal;
        TypeRattachement = typeRattachement;
        IsActive = isActive;
    }

    public static RattachementEmploye QueryCreate(
        RattachementEmployeId rattachementEmployeId,
        RattachementId rattachementId,
        string matricule,
        string? nomPrenom,
        DateTime? dateDebut,
        TimeSpan? heureDebut,
        DateTime? dateFin,
        TimeSpan? heureFin,
        decimal? nombreHeure,
        decimal? cout,
        decimal? coutGlobal,
        string? typeRattachement,
        bool isActive,
        SocieteId societeId)
    {
        return new RattachementEmploye(
            rattachementEmployeId,
            rattachementId,
            matricule,
            nomPrenom,
            dateDebut,
            heureDebut,
            dateFin,
            heureFin,
            nombreHeure,
            cout,
            coutGlobal,
            typeRattachement,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private RattachementEmploye() { }
#pragma warning restore CS8618
}
