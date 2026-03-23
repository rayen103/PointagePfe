using CollectManagement.Application.Interfaces.Repositories.Chantiers;
using CollectManagement.Domain.Chantiers.ValueObjects;

namespace CollectManagement.Application.Features.Chantiers.Commands.DeleteChantier;

public class DeleteChantierCommandHandler : IRequestHandler<DeleteChantierCommand, Unit>
{
    private readonly IChantierRepository _chantierRepository;

    public DeleteChantierCommandHandler(IChantierRepository chantierRepository)
    {
        _chantierRepository = chantierRepository;
    }

    public async Task<Unit> Handle(DeleteChantierCommand request, CancellationToken cancellationToken)
    {
        var chantierId = new ChantierId(request.ChantierId);
        await _chantierRepository.DeleteAsync(c => c.ChantierId == chantierId, cancellationToken).ConfigureAwait(false);
        return Unit.Value;
    }
}
