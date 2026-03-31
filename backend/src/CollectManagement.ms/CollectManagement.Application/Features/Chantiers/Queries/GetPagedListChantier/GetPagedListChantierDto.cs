namespace CollectManagement.Application.Features.Chantiers.Queries.GetPagedListChantier;

public class GetPagedListChantierDto
{
    public Ulid ChantierId { get; set; }
    public string NumeroChantier { get; set; } = string.Empty;
    public string? LibelleChantier { get; set; }
    public string? CodeClient { get; set; }
    public string? Nature { get; set; }
    public string? Responsable { get; set; }
    public string? Status { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
