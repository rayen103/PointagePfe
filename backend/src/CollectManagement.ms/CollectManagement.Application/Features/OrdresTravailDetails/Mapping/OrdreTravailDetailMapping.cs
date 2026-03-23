using CollectManagement.Application.Features.OrdresTravailDetails.Commands.CreateOrdreTravailDetail;
using CollectManagement.Application.Features.OrdresTravailDetails.Commands.UpdateOrdreTravailDetail;
using CollectManagement.Application.Features.OrdresTravailDetails.Queries.GetByOrdreTravail;
using CollectManagement.Domain.OrdresTravail;

namespace CollectManagement.Application.Features.OrdresTravailDetails.Mapping;

public class OrdreTravailDetailMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrdreTravailDetail, CreateOrdreTravailDetailResponse>()
            .Map(d => d.OrdreTravailDetailId, s => s.OrdreTravailDetailId.Value)
            .Map(d => d.OrdreTravailId, s => s.OrdreTravailId.Value);

        config.NewConfig<OrdreTravailDetail, UpdateOrdreTravailDetailResponse>()
            .Map(d => d.OrdreTravailDetailId, s => s.OrdreTravailDetailId.Value)
            .Map(d => d.OrdreTravailId, s => s.OrdreTravailId.Value);

        config.NewConfig<OrdreTravailDetail, GetByOrdreTravailDto>()
            .Map(d => d.OrdreTravailDetailId, s => s.OrdreTravailDetailId.Value)
            .Map(d => d.OrdreTravailId, s => s.OrdreTravailId.Value);
    }
}
