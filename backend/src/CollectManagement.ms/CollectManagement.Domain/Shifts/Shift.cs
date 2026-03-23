using CollectManagement.Domain.Common;
using CollectManagement.Domain.Shifts.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Shifts;

public class Shift : AuditableEntity
{
    public ShiftId ShiftId { get; private set; }

    public string CodeShift { get; private set; }

    public string? LibelleShift { get; private set; }

    public string? JourSemaine { get; private set; }

    public TimeSpan? HeureDebut { get; private set; }

    public TimeSpan? HeureFin { get; private set; }

    public bool IsActive { get; private set; } = true;

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Shift(
        ShiftId shiftId,
        string codeShift,
        string? libelleShift,
        string? jourSemaine,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        bool isActive,
        SocieteId societeId)
    {
        ShiftId = shiftId;
        CodeShift = codeShift;
        LibelleShift = libelleShift;
        JourSemaine = jourSemaine;
        HeureDebut = heureDebut;
        HeureFin = heureFin;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Shift Create(
        ShiftId shiftId,
        string codeShift,
        string? libelleShift,
        string? jourSemaine,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        bool isActive,
        SocieteId societeId)
    {
        return new Shift(
            shiftId,
            codeShift,
            libelleShift,
            jourSemaine,
            heureDebut,
            heureFin,
            isActive,
            societeId);
    }

    public void Update(
        string codeShift,
        string? libelleShift,
        string? jourSemaine,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        bool isActive)
    {
        CodeShift = codeShift;
        LibelleShift = libelleShift;
        JourSemaine = jourSemaine;
        HeureDebut = heureDebut;
        HeureFin = heureFin;
        IsActive = isActive;
    }

    public static Shift QueryCreate(
        ShiftId shiftId,
        string codeShift,
        string? libelleShift,
        string? jourSemaine,
        TimeSpan? heureDebut,
        TimeSpan? heureFin,
        bool isActive,
        SocieteId societeId)
    {
        return new Shift(
            shiftId,
            codeShift,
            libelleShift,
            jourSemaine,
            heureDebut,
            heureFin,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Shift() { }
#pragma warning restore CS8618
}
