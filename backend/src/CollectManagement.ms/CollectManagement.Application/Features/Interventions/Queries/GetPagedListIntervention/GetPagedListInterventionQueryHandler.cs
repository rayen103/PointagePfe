using CollectManagement.Application.Interfaces.Repositories.Interventions;

namespace CollectManagement.Application.Features.Interventions.Queries.GetPagedListIntervention;

public class GetPagedListInterventionQueryHandler : IRequestHandler<GetPagedListInterventionQuery, GetPagedListInterventionResponse>
{
    private readonly IInterventionRepository _interventionRepository;
    private readonly IMapper _mapper;

    public GetPagedListInterventionQueryHandler(IInterventionRepository interventionRepository, IMapper mapper)
    {
        _interventionRepository = interventionRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListInterventionResponse> Handle(GetPagedListInterventionQuery request, CancellationToken cancellationToken)
    {
        var (listIntervention, length) = await _interventionRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken
            )
            .ConfigureAwait(false);

        return new GetPagedListInterventionResponse(
            _mapper.Map<List<GetPagedListInterventionDto>>(listIntervention),
            length
        );
    }
}
