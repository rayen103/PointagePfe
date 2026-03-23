using CollectManagement.Application.Interfaces.Repositories.OrdresTravailDetails;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravailDetails.Queries.GetByOrdreTravail;

public class GetByOrdreTravailQueryHandler
    : IRequestHandler<GetByOrdreTravailQuery, GetByOrdreTravailResponse>
{
    private readonly IOrdreTravailDetailRepository _repository;
    private readonly IMapper _mapper;

    public GetByOrdreTravailQueryHandler(
        IOrdreTravailDetailRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByOrdreTravailResponse> Handle(
        GetByOrdreTravailQuery request,
        CancellationToken cancellationToken)
    {
        var ordreTravailId = new OrdreTravailId(request.OrdreTravailId);

        var items = await _repository
            .GetByOrdreTravailAsync(ordreTravailId, cancellationToken)
            .ConfigureAwait(false);

        var dtos = _mapper.Map<IReadOnlyList<GetByOrdreTravailDto>>(items);

        return new GetByOrdreTravailResponse { Items = dtos };
    }
}
