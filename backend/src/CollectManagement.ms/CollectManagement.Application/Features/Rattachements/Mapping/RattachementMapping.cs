using CollectManagement.Application.Features.Rattachements.Commands.CreateRattachement;
using CollectManagement.Application.Features.Rattachements.Commands.UpdateRattachement;
using CollectManagement.Application.Features.Rattachements.Queries.GetOneRattachement;
using CollectManagement.Application.Features.Rattachements.Queries.GetPagedListRattachement;
using CollectManagement.Domain.Rattachements;

namespace CollectManagement.Application.Features.Rattachements.Mapping;

public class RattachementMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Rattachement, CreateRattachementResponse>()
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Rattachement, UpdateRattachementResponse>()
            .Map(d => d.RattachementId, s => s.RattachementId.Value);

        config.NewConfig<Rattachement, GetPagedListRattachementDto>()
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Rattachement, GetOneRattachementDto>()
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
