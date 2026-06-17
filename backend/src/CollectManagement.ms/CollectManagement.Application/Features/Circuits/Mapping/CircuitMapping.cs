using CollectManagement.Application.Features.Circuits.Commands.CreateCircuit;
using CollectManagement.Application.Features.Circuits.Commands.UpdateCircuit;
using CollectManagement.Application.Features.Circuits.Queries.GetOneCircuit;
using CollectManagement.Application.Features.Circuits.Queries.GetPagedListCircuit;
using CollectManagement.Domain.Circuits;

namespace CollectManagement.Application.Features.Circuits.Mapping;

public class CircuitMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Circuit, CreateCircuitResponse>()
            .Map(d => d.CircuitId, s => s.CircuitId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Circuit, UpdateCircuitResponse>()
            .Map(d => d.CircuitId, s => s.CircuitId.Value);

        config.NewConfig<Circuit, GetPagedListCircuitDto>()
            .Map(d => d.CircuitId, s => s.CircuitId.Value)
            .Map(d => d.CodeCircuit, s => s.CodeCircuit)
            .Map(d => d.LibelleCircuit, s => s.LibelleCircuit)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Latitude, s => s.Latitude)
            .Map(d => d.Longitude, s => s.Longitude)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.SocieteId, s => s.SocieteId.Value)
            .Map(d => d.CodePCDepart, s => s.CodePCDepart)
            .Map(d => d.CodePCArrivee, s => s.CodePCArrivee)
            .Map(d => d.DistanceKm, s => s.DistanceKm)
            .Map(d => d.DureeMinutes, s => s.DureeMinutes)
            .Map(d => d.Couleur, s => s.Couleur);

        config.NewConfig<Circuit, GetOneCircuitDto>()
            .Map(d => d.CircuitId, s => s.CircuitId.Value)
            .Map(d => d.CodeCircuit, s => s.CodeCircuit)
            .Map(d => d.LibelleCircuit, s => s.LibelleCircuit)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.Latitude, s => s.Latitude)
            .Map(d => d.Longitude, s => s.Longitude)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.SocieteId, s => s.SocieteId.Value)
            .Map(d => d.CodePCDepart, s => s.CodePCDepart)
            .Map(d => d.CodePCArrivee, s => s.CodePCArrivee)
            .Map(d => d.DistanceKm, s => s.DistanceKm)
            .Map(d => d.DureeMinutes, s => s.DureeMinutes)
            .Map(d => d.Couleur, s => s.Couleur);
    }
}
