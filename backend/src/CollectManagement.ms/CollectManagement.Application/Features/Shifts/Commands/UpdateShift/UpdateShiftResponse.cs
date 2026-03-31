namespace CollectManagement.Application.Features.Shifts.Commands.UpdateShift;

public class UpdateShiftResponse
{
    public Ulid ShiftId { get; set; }
    public string CodeShift { get; set; } = string.Empty;
    public string? LibelleShift { get; set; }
    public string? JourSemaine { get; set; }
    public TimeSpan? HeureDebut { get; set; }
    public TimeSpan? HeureFin { get; set; }
    public bool IsActive { get; set; }
}
