using CollectManagement.Application.Features.Interventions.Commands.CreateIntervention;
using CollectManagement.Application.Features.Interventions.Queries.GetOneIntervention;
using CollectManagement.Application.Features.Interventions.Queries.GetPagedListIntervention;
using CollectManagement.Domain.Interventions;

namespace CollectManagement.Application.Features.Interventions.Mapping;

public class InterventionMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Intervention, CreateInterventionResponse>()
            .Map(d => d.InterventionId, s => s.InterventionId.Value);

        config.NewConfig<Intervention, GetPagedListInterventionDto>()
            .Map(d => d.InterventionId, s => s.InterventionId.Value)
            .Map(d => d.NumeroIntervention, s => s.NumeroIntervention)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.DateIntervention, s => s.DateIntervention)
            .Map(d => d.TypeIntervention, s => s.TypeIntervention)
            .Map(d => d.Statut, s => s.Statut)
            .Map(d => d.Cout, s => s.Cout);

        config.NewConfig<Intervention, GetOneInterventionResponse>()
            .Map(d => d.InterventionId, s => s.InterventionId.Value)
            .Map(d => d.NumeroIntervention, s => s.NumeroIntervention)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.DateIntervention, s => s.DateIntervention)
            .Map(d => d.TypeIntervention, s => s.TypeIntervention)
            .Map(d => d.Statut, s => s.Statut)
            .Map(d => d.Cout, s => s.Cout);
    }
}
