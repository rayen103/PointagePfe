using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.Rattachements.Commands.DeleteRattachement;

public class DeleteRattachementCommandHandler
    : IRequestHandler<DeleteRattachementCommand, Unit>
{
    private readonly IRattachementRepository _rattachementRepository;

    public DeleteRattachementCommandHandler(IRattachementRepository rattachementRepository)
    {
        _rattachementRepository = rattachementRepository;
    }

    public async Task<Unit> Handle(DeleteRattachementCommand request, CancellationToken cancellationToken)
    {
        var rattachementId = new RattachementId(request.RattachementId);

        await _rattachementRepository
            .DeleteAsync(c => c.RattachementId == rattachementId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
