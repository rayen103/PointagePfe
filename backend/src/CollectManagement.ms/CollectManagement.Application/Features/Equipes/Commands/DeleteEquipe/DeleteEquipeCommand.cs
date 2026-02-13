namespace CollectManagement.Application.Features.Equipes.Commands.DeleteEquipe;

public record DeleteEquipeCommand(Ulid EquipeId) : IRequest<Unit>;
