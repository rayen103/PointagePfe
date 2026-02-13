using CollectManagement.Application.Features.OrdresTravail.Commands.CreateOrdreTravail;
using CollectManagement.Application.Features.OrdresTravail.Commands.UpdateOrdreTravail;
using CollectManagement.Application.Features.OrdresTravail.Queries.GetOneOrdreTravail;
using CollectManagement.Application.Features.OrdresTravail.Queries.GetPagedListOrdreTravail;
using CollectManagement.Domain.OrdresTravail;

namespace CollectManagement.Application.Features.OrdresTravail.Mapping;

public class OrdreTravailMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrdreTravail, CreateOrdreTravailResponse>()
            .Map(d => d.OrdreTravailId, s => s.OrdreTravailId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<OrdreTravail, UpdateOrdreTravailResponse>()
            .Map(d => d.OrdreTravailId, s => s.OrdreTravailId.Value);

        config.NewConfig<OrdreTravail, GetPagedListOrdreTravailDto>()
            .Map(d => d.OrdreTravailId, s => s.OrdreTravailId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<OrdreTravail, GetOneOrdreTravailDto>()
            .Map(d => d.OrdreTravailId, s => s.OrdreTravailId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
