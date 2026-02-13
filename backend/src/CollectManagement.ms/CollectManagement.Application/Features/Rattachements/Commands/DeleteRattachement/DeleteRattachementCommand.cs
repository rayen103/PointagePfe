namespace CollectManagement.Application.Features.Rattachements.Commands.DeleteRattachement;

public record DeleteRattachementCommand(Ulid RattachementId) : IRequest<Unit>;
