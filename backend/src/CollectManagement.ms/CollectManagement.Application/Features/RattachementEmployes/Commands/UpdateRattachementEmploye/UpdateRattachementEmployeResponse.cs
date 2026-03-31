namespace CollectManagement.Application.Features.RattachementEmployes.Commands.UpdateRattachementEmploye;

public class UpdateRattachementEmployeResponse
{
    public Ulid RattachementEmployeId { get; set; }
    public Ulid RattachementId { get; set; }
    public string Matricule { get; set; } = string.Empty;
}
