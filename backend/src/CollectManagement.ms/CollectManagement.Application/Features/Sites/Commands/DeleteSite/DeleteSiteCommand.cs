namespace CollectManagement.Application.Features.Sites.Commands.DeleteSite;

public record DeleteSiteCommand(Ulid SiteId) : IRequest;
