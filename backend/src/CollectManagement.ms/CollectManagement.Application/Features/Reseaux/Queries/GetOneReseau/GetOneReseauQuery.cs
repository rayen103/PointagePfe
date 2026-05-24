namespace CollectManagement.Application.Features.Reseaux.Queries.GetOneReseau;

public record GetOneReseauQuery(Ulid ReseauId) : IRequest<GetOneReseauDto>;
