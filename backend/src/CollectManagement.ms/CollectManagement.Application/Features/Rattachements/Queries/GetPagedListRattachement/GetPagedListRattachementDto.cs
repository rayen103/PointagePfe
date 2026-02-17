namespace CollectManagement.Application.Features.Rattachements.Queries.GetPagedListRattachement;

public class GetPagedListRattachementDto
{
    public Ulid RattachementId { get; set; }
    public string NumeroRattachement { get; set; } = string.Empty;
    public int? Exercice { get; set; }
    public DateTime DateRattachement { get; set; }
    public string? NumeroChantier { get; set; }
    public string? CodeClient { get; set; }
    public bool IsInternal { get; set; }
    public decimal? Cout { get; set; }
    public string? Type { get; set; }
    public string? Nature { get; set; }
    public string? Responsable { get; set; }
    public string? Status { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
