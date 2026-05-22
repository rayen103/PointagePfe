using CollectManagement.Application.Interfaces.Repositories.Reseaux;
using CollectManagement.Domain.Reseaux.ValueObjects;

namespace CollectManagement.Application.Features.Reseaux.Commands.DeleteReseau;

public class DeleteReseauCommandHandler : IRequestHandler<DeleteReseauCommand>
{
    private readonly IReseauRepository _reseauRepository;

    public DeleteReseauCommandHandler(IReseauRepository reseauRepository)
    {
        _reseauRepository = reseauRepository;
    }

    public async Task Handle(DeleteReseauCommand request, CancellationToken cancellationToken)
    {
        await _reseauRepository.DeleteAsync(x => x.ReseauId == new ReseauId(request.ReseauId), cancellationToken).ConfigureAwait(false);
    }
}
