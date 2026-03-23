using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Domain.Bus.ValueObjects;

namespace CollectManagement.Application.Features.Bus.Queries.GetOneBus;

public class GetOneBusQueryHandler
    : IRequestHandler<GetOneBusQuery, GetOneBusDto>
{
    private readonly IBusRepository _busRepository;
    private readonly IMapper _mapper;

    public GetOneBusQueryHandler(
        IBusRepository busRepository,
        IMapper mapper)
    {
        _busRepository = busRepository;
        _mapper = mapper;
    }

    public async Task<GetOneBusDto> Handle(GetOneBusQuery request, CancellationToken cancellationToken)
    {
        var busId = new BusId(request.BusId);

        var bus = await _busRepository
            .GetOneAsync(busId, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<GetOneBusDto>(bus);
    }
}
