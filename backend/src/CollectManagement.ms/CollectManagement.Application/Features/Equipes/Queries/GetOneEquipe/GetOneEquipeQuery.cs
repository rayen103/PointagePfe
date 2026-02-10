namespace CollectManagement.Application.Features.Equipes.Queries.GetOneEquipe;

public record GetOneEquipeQuery(Ulid EquipeId) : IRequest<GetOneEquipeDto>;
