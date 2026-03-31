namespace CollectManagement.Application.Features.Shifts.Queries.GetPagedListShift;

public class GetPagedListShiftDto
{
    public Ulid ShiftId { get; set; }
    public string CodeShift { get; set; } = string.Empty;
    public string? LibelleShift { get; set; }
    public string? JourSemaine { get; set; }
    public TimeSpan? HeureDebut { get; set; }
    public TimeSpan? HeureFin { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
