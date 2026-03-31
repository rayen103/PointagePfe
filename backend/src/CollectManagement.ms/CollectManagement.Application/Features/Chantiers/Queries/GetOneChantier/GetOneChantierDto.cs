namespace CollectManagement.Application.Features.Chantiers.Queries.GetOneChantier;

public class GetOneChantierDto
{
    public Ulid ChantierId { get; set; }
    public string NumeroChantier { get; set; } = string.Empty;
    public string? LibelleChantier { get; set; }
    public string? CodeClient { get; set; }
    public string? Adresse { get; set; }
    public decimal? MontantHT { get; set; }
    public decimal? MontantTTC { get; set; }
    public string? Nature { get; set; }
    public string? Responsable { get; set; }
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public string? Status { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
