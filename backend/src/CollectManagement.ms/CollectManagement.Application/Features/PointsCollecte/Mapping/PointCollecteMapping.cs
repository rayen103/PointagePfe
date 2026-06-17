using CollectManagement.Application.Features.PointsCollecte.Commands.CreatePointCollecte;
using CollectManagement.Application.Features.PointsCollecte.Commands.UpdatePointCollecte;
using CollectManagement.Application.Features.PointsCollecte.Queries.GetOnePointCollecte;
using CollectManagement.Application.Features.PointsCollecte.Queries.GetPagedListPointCollecte;
using CollectManagement.Domain.PointsCollecte;

namespace CollectManagement.Application.Features.PointsCollecte.Mapping;

public class PointCollecteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PointCollecte, CreatePointCollecteResponse>()
            .Map(d => d.PointCollecteId, s => s.PointCollecteId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value)
            .Map(d => d.CircuitId, s => s.CircuitId == null ? null : (Ulid?)s.CircuitId.Value);

        config.NewConfig<PointCollecte, UpdatePointCollecteResponse>()
            .Map(d => d.PointCollecteId, s => s.PointCollecteId.Value);

        config.NewConfig<PointCollecte, GetPagedListPointCollecteDto>()
            .Map(d => d.PointCollecteId, s => s.PointCollecteId.Value)
            .Map(d => d.CodePointCollecte, s => s.CodePointCollecte)
            .Map(d => d.LibellePointCollecte, s => s.LibellePointCollecte)
            .Map(d => d.Latitude, s => s.Latitude)
            .Map(d => d.Longitude, s => s.Longitude)
            .Map(d => d.CodeGouvernorat, s => s.CodeGouvernorat)
            .Map(d => d.CodeRegion, s => s.CodeRegion)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.SocieteId, s => s.SocieteId.Value)
            .Map(d => d.CircuitId, s => s.CircuitId == null ? null : (Ulid?)s.CircuitId.Value);

        config.NewConfig<PointCollecte, GetOnePointCollecteDto>()
            .Map(d => d.PointCollecteId, s => s.PointCollecteId.Value)
            .Map(d => d.CodePointCollecte, s => s.CodePointCollecte)
            .Map(d => d.LibellePointCollecte, s => s.LibellePointCollecte)
            .Map(d => d.Latitude, s => s.Latitude)
            .Map(d => d.Longitude, s => s.Longitude)
            .Map(d => d.CodeGouvernorat, s => s.CodeGouvernorat)
            .Map(d => d.CodeRegion, s => s.CodeRegion)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.SocieteId, s => s.SocieteId.Value)
            .Map(d => d.CircuitId, s => s.CircuitId == null ? null : (Ulid?)s.CircuitId.Value);
    }
}
