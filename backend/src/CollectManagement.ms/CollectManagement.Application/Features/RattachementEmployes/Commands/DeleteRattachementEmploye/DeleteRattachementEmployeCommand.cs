namespace CollectManagement.Application.Features.RattachementEmployes.Commands.DeleteRattachementEmploye;

public record DeleteRattachementEmployeCommand(Ulid RattachementEmployeId) : IRequest<Unit>;
