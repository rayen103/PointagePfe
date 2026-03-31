using CollectManagement.Application.Interfaces.Repositories.OrdresTravailDetails;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravailDetails.Commands.DeleteOrdreTravailDetail;

public class DeleteOrdreTravailDetailCommandHandler
    : IRequestHandler<DeleteOrdreTravailDetailCommand, Unit>
{
    private readonly IOrdreTravailDetailRepository _repository;

    public DeleteOrdreTravailDetailCommandHandler(IOrdreTravailDetailRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        DeleteOrdreTravailDetailCommand request,
        CancellationToken cancellationToken)
    {
        var id = new OrdreTravailDetailId(request.OrdreTravailDetailId);

        await _repository
            .DeleteAsync(c => c.OrdreTravailDetailId == id, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
