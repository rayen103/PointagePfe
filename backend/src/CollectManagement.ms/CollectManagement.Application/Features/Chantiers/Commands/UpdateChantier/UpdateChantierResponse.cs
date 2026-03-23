namespace CollectManagement.Application.Features.Chantiers.Commands.UpdateChantier;

public class UpdateChantierResponse
{
    public Ulid ChantierId { get; set; }
    public string NumeroChantier { get; set; } = string.Empty;
}
