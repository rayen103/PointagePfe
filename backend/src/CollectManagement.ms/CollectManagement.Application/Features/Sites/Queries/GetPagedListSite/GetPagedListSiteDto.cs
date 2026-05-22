namespace CollectManagement.Application.Features.Sites.Queries.GetPagedListSite;

public class GetPagedListSiteDto
{
    public Ulid SiteId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;
    public bool Siege { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Rayon { get; set; }
    public int? TimeMinute { get; set; }
    public bool IsActive { get; set; }
    public Ulid SocieteId { get; set; }
}
