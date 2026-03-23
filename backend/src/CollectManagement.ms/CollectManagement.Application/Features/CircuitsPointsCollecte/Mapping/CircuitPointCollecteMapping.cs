using CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.CreateCircuitPointCollecte;
using CollectManagement.Application.Features.CircuitsPointsCollecte.Commands.UpdateCircuitPointCollecte;
using CollectManagement.Application.Features.CircuitsPointsCollecte.Queries.GetByCircuit;
using CollectManagement.Domain.Circuits;

namespace CollectManagement.Application.Features.CircuitsPointsCollecte.Mapping;

public class CircuitPointCollecteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CircuitPointCollecte, CreateCircuitPointCollecteResponse>()
            .Map(d => d.CircuitPointCollecteId, s => s.CircuitPointCollecteId.Value)
            .Map(d => d.CircuitId, s => s.CircuitId.Value);

        config.NewConfig<CircuitPointCollecte, UpdateCircuitPointCollecteResponse>()
            .Map(d => d.CircuitPointCollecteId, s => s.CircuitPointCollecteId.Value)
            .Map(d => d.CircuitId, s => s.CircuitId.Value);

        config.NewConfig<CircuitPointCollecte, GetByCircuitDto>()
            .Map(d => d.CircuitPointCollecteId, s => s.CircuitPointCollecteId.Value)
            .Map(d => d.CircuitId, s => s.CircuitId.Value);
    }
}
