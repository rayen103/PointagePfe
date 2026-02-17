namespace CollectManagement.Application.Features.Rattachements.Commands.CreateRattachement;

public class CreateRattachementResponse
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
    public TimeSpan? HeureDebut { get; set; }
    public TimeSpan? HeureFin { get; set; }
    public string? Emplacement { get; set; }
    public string? Reference { get; set; }
    public string? Status { get; set; }
    public DateTime? DateCloture { get; set; }
    public string? Remarque { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
