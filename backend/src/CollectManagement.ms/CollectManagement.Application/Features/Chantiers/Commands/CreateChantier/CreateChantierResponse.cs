namespace CollectManagement.Application.Features.Chantiers.Commands.CreateChantier;

public class CreateChantierResponse
{
    public Ulid ChantierId { get; set; }
    public string NumeroChantier { get; set; } = string.Empty;
    public Ulid SocieteId { get; set; }
}
