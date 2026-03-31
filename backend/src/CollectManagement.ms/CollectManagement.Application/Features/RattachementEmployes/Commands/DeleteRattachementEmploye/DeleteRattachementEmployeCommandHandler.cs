using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.RattachementEmployes.Commands.DeleteRattachementEmploye;

public class DeleteRattachementEmployeCommandHandler
    : IRequestHandler<DeleteRattachementEmployeCommand, Unit>
{
    private readonly IRattachementEmployeRepository _rattachementEmployeRepository;

    public DeleteRattachementEmployeCommandHandler(
        IRattachementEmployeRepository rattachementEmployeRepository)
    {
        _rattachementEmployeRepository = rattachementEmployeRepository;
    }

    public async Task<Unit> Handle(
        DeleteRattachementEmployeCommand request,
        CancellationToken cancellationToken)
    {
        var rattachementEmployeId = new RattachementEmployeId(request.RattachementEmployeId);

        await _rattachementEmployeRepository
            .DeleteAsync(c => c.RattachementEmployeId == rattachementEmployeId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
