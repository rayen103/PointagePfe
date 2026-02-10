using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Interventions;
using CollectManagement.Domain.Interventions.ValueObjects;

namespace CollectManagement.Application.Features.Interventions.Queries.GetOneIntervention;

public class GetOneInterventionQueryHandler : IRequestHandler<GetOneInterventionQuery, GetOneInterventionResponse>
{
    private readonly IInterventionRepository _interventionRepository;
    private readonly IMapper _mapper;

    public GetOneInterventionQueryHandler(IInterventionRepository interventionRepository, IMapper mapper)
    {
        _interventionRepository = interventionRepository;
        _mapper = mapper;
    }

    public async Task<GetOneInterventionResponse> Handle(GetOneInterventionQuery request, CancellationToken cancellationToken)
    {
        var interventionId = new InterventionId(request.InterventionId);

        var intervention = await _interventionRepository
            .GetOneAsync(interventionId, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException("Intervention NotFound.");

        return _mapper.Map<GetOneInterventionResponse>(intervention);
    }
}
