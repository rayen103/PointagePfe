using CollectManagement.Application.Features.Bus.Commands.CreateBus;
using CollectManagement.Application.Features.Bus.Commands.UpdateBus;
using CollectManagement.Application.Features.Bus.Queries.GetOneBus;
using CollectManagement.Application.Features.Bus.Queries.GetPagedListBus;

namespace CollectManagement.Application.Features.Bus.Mapping;

public class BusMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Bus.Bus, CreateBusResponse>()
            .Map(d => d.BusId, s => s.BusId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Domain.Bus.Bus, UpdateBusResponse>()
            .Map(d => d.BusId, s => s.BusId.Value);

        config.NewConfig<Domain.Bus.Bus, GetPagedListBusDto>()
            .Map(d => d.BusId, s => s.BusId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Domain.Bus.Bus, GetOneBusDto>()
            .Map(d => d.BusId, s => s.BusId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
