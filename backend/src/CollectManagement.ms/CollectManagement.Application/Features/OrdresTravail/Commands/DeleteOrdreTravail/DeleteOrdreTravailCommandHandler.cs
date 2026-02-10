using CollectManagement.Application.Interfaces.Repositories.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravail.Commands.DeleteOrdreTravail;

public class DeleteOrdreTravailCommandHandler
    : IRequestHandler<DeleteOrdreTravailCommand, Unit>
{
    private readonly IOrdreTravailRepository _ordreTravailRepository;

    public DeleteOrdreTravailCommandHandler(IOrdreTravailRepository ordreTravailRepository)
    {
        _ordreTravailRepository = ordreTravailRepository;
    }

    public async Task<Unit> Handle(DeleteOrdreTravailCommand request, CancellationToken cancellationToken)
    {
        var ordreTravailId = new OrdreTravailId(request.OrdreTravailId);

        await _ordreTravailRepository
            .DeleteAsync(c => c.OrdreTravailId == ordreTravailId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
