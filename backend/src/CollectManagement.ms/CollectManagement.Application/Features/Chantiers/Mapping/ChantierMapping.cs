using CollectManagement.Application.Features.Chantiers.Commands.CreateChantier;
using CollectManagement.Application.Features.Chantiers.Commands.UpdateChantier;
using CollectManagement.Application.Features.Chantiers.Queries.GetOneChantier;
using CollectManagement.Application.Features.Chantiers.Queries.GetPagedListChantier;
using CollectManagement.Domain.Chantiers;

namespace CollectManagement.Application.Features.Chantiers.Mapping;

public class ChantierMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Chantier, CreateChantierResponse>()
            .Map(d => d.ChantierId, s => s.ChantierId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Chantier, UpdateChantierResponse>()
            .Map(d => d.ChantierId, s => s.ChantierId.Value);

        config.NewConfig<Chantier, GetPagedListChantierDto>()
            .Map(d => d.ChantierId, s => s.ChantierId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Chantier, GetOneChantierDto>()
            .Map(d => d.ChantierId, s => s.ChantierId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
