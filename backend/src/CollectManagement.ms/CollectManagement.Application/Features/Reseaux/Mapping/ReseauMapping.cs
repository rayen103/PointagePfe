using CollectManagement.Application.Features.Reseaux.Commands.CreateReseau;
using CollectManagement.Application.Features.Reseaux.Commands.UpdateReseau;
using CollectManagement.Application.Features.Reseaux.Queries.GetOneReseau;
using CollectManagement.Application.Features.Reseaux.Queries.GetPagedListReseau;
using CollectManagement.Domain.Reseaux;

namespace CollectManagement.Application.Features.Reseaux.Mapping;

public class ReseauMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Reseau, CreateReseauResponse>()
            .Map(d => d.ReseauId, s => s.ReseauId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Reseau, UpdateReseauResponse>()
            .Map(d => d.ReseauId, s => s.ReseauId.Value);

        config.NewConfig<Reseau, GetOneReseauDto>()
            .Map(d => d.ReseauId, s => s.ReseauId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Reseau, GetPagedListReseauDto>()
            .Map(d => d.ReseauId, s => s.ReseauId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
