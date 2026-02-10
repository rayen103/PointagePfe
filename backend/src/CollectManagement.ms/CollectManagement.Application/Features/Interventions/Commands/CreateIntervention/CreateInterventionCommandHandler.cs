using CollectManagement.Application.Interfaces.Repositories.Interventions;
using CollectManagement.Domain.Interventions;
using CollectManagement.Domain.Interventions.ValueObjects;

namespace CollectManagement.Application.Features.Interventions.Commands.CreateIntervention;

public class CreateInterventionCommandHandler : IRequestHandler<CreateInterventionCommand, CreateInterventionResponse>
{
    private readonly IInterventionRepository _interventionRepository;
    private readonly IMapper _mapper;

    public CreateInterventionCommandHandler(
        IInterventionRepository interventionRepository,
        IMapper mapper)
    {
        _interventionRepository = interventionRepository;
        _mapper = mapper;
    }

    public async Task<CreateInterventionResponse> Handle(CreateInterventionCommand request, CancellationToken cancellationToken)
    {
        var interventionId = new InterventionId(Ulid.NewUlid());

        var intervention = Intervention.Create(
            interventionId,
            request.NumeroIntervention,
            request.Description,
            request.DateIntervention,
            request.TypeIntervention,
            request.Statut,
            request.Cout
        );

        await _interventionRepository
            .AddAsync(intervention, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateInterventionResponse>(intervention);
    }
}
