namespace CollectManagement.Application.Features.RattachementEmployes.Commands.CreateRattachementEmploye;

public class CreateRattachementEmployeResponse
{
    public Ulid RattachementEmployeId { get; set; }
    public Ulid RattachementId { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public Ulid SocieteId { get; set; }
}
