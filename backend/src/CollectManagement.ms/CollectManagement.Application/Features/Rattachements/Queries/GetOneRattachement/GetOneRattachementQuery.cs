namespace CollectManagement.Application.Features.Rattachements.Queries.GetOneRattachement;

public record GetOneRattachementQuery(Ulid RattachementId) : IRequest<GetOneRattachementDto>;
