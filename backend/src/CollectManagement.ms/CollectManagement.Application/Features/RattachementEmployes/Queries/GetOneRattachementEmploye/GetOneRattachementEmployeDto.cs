namespace CollectManagement.Application.Features.RattachementEmployes.Queries.GetOneRattachementEmploye;

public class GetOneRattachementEmployeDto
{
    public Ulid RattachementEmployeId { get; set; }
    public Ulid RattachementId { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string? NomPrenom { get; set; }
    public DateTime? DateDebut { get; set; }
    public TimeSpan? HeureDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public TimeSpan? HeureFin { get; set; }
    public decimal? NombreHeure { get; set; }
    public decimal? Cout { get; set; }
    public decimal? CoutGlobal { get; set; }
    public string? TypeRattachement { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
