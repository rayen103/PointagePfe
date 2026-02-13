using CollectManagement.Application.Interfaces.Repositories.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Application.Features.OrdresTravail.Queries.GetOneOrdreTravail;

public class GetOneOrdreTravailQueryHandler
    : IRequestHandler<GetOneOrdreTravailQuery, GetOneOrdreTravailDto>
{
    private readonly IOrdreTravailRepository _ordreTravailRepository;
    private readonly IMapper _mapper;

    public GetOneOrdreTravailQueryHandler(
        IOrdreTravailRepository ordreTravailRepository,
        IMapper mapper)
    {
        _ordreTravailRepository = ordreTravailRepository;
        _mapper = mapper;
    }

    public async Task<GetOneOrdreTravailDto> Handle(GetOneOrdreTravailQuery request, CancellationToken cancellationToken)
    {
        var ordreTravailId = new OrdreTravailId(request.OrdreTravailId);

        var ordreTravail = await _ordreTravailRepository
            .GetOneAsync(ordreTravailId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOneOrdreTravailDto>(ordreTravail);
    }
}
