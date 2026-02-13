using CollectManagement.Application.Features.Equipes.Commands.CreateEquipe;
using CollectManagement.Application.Features.Equipes.Commands.UpdateEquipe;
using CollectManagement.Application.Features.Equipes.Queries.GetOneEquipe;
using CollectManagement.Application.Features.Equipes.Queries.GetPagedListEquipe;
using CollectManagement.Domain.Equipes;

namespace CollectManagement.Application.Features.Equipes.Mapping;

public class EquipeMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Equipe, CreateEquipeResponse>()
            .Map(d => d.EquipeId, s => s.EquipeId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Equipe, UpdateEquipeResponse>()
            .Map(d => d.EquipeId, s => s.EquipeId.Value);

        config.NewConfig<Equipe, GetPagedListEquipeDto>()
            .Map(d => d.EquipeId, s => s.EquipeId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Equipe, GetOneEquipeDto>()
            .Map(d => d.EquipeId, s => s.EquipeId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
