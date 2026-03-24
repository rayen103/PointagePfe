using CollectManagement.Application.Features.RattachementEmployes.Commands.CreateRattachementEmploye;
using CollectManagement.Application.Features.RattachementEmployes.Commands.UpdateRattachementEmploye;
using CollectManagement.Application.Features.RattachementEmployes.Queries.GetOneRattachementEmploye;
using CollectManagement.Application.Features.RattachementEmployes.Queries.GetPagedListRattachementEmploye;
using CollectManagement.Domain.Rattachements;

namespace CollectManagement.Application.Features.RattachementEmployes.Mapping;

public class RattachementEmployeMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RattachementEmploye, CreateRattachementEmployeResponse>()
            .Map(d => d.RattachementEmployeId, s => s.RattachementEmployeId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<RattachementEmploye, UpdateRattachementEmployeResponse>()
            .Map(d => d.RattachementEmployeId, s => s.RattachementEmployeId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value);

        config.NewConfig<RattachementEmploye, GetPagedListRattachementEmployeDto>()
            .Map(d => d.RattachementEmployeId, s => s.RattachementEmployeId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<RattachementEmploye, GetOneRattachementEmployeDto>()
            .Map(d => d.RattachementEmployeId, s => s.RattachementEmployeId.Value)
            .Map(d => d.RattachementId, s => s.RattachementId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
