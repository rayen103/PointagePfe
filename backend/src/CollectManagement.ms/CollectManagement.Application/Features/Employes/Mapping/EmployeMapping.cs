using CollectManagement.Application.Features.Employes.Commands.CreateEmploye;
using CollectManagement.Application.Features.Employes.Queries.GetOneEmploye;
using CollectManagement.Application.Features.Employes.Queries.GetPagedListEmploye;
using CollectManagement.Domain.Employes;

namespace CollectManagement.Application.Features.Employes.Mapping;

public class EmployeMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Employe, CreateEmployeResponse>()
            .Map(d => d.EmployeId, s => s.EmployeId.Value)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Employe, GetPagedListEmployeDto>()
            .Map(d => d.EmployeId, s => s.EmployeId.Value)
            .Map(d => d.Matricule, s => s.Matricule)
            .Map(d => d.RFID, s => s.RFID)
            .Map(d => d.Nom, s => s.Nom)
            .Map(d => d.Prenom, s => s.Prenom)
            .Map(d => d.TypeEmploye, s => s.TypeEmploye)
            .Map(d => d.CodeCircuit, s => s.CodeCircuit)
            .Map(d => d.CodePointCollecte, s => s.CodePointCollecte)
            .Map(d => d.CodeBus, s => s.CodeBus)
            .Map(d => d.CodeShift, s => s.CodeShift)
            .Map(d => d.Adresse, s => s.Adresse)
            .Map(d => d.CodeGouvernorat, s => s.CodeGouvernorat)
            .Map(d => d.CodeRegion, s => s.CodeRegion)
            .Map(d => d.Latitude, s => s.Latitude)
            .Map(d => d.Longitude, s => s.Longitude)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);

        config.NewConfig<Employe, GetOneEmployeResponse>()
            .Map(d => d.EmployeId, s => s.EmployeId.Value)
            .Map(d => d.Matricule, s => s.Matricule)
            .Map(d => d.RFID, s => s.RFID)
            .Map(d => d.Nom, s => s.Nom)
            .Map(d => d.Prenom, s => s.Prenom)
            .Map(d => d.TypeEmploye, s => s.TypeEmploye)
            .Map(d => d.CodeCircuit, s => s.CodeCircuit)
            .Map(d => d.CodePointCollecte, s => s.CodePointCollecte)
            .Map(d => d.CodeBus, s => s.CodeBus)
            .Map(d => d.CodeShift, s => s.CodeShift)
            .Map(d => d.Adresse, s => s.Adresse)
            .Map(d => d.CodeGouvernorat, s => s.CodeGouvernorat)
            .Map(d => d.CodeRegion, s => s.CodeRegion)
            .Map(d => d.Latitude, s => s.Latitude)
            .Map(d => d.Longitude, s => s.Longitude)
            .Map(d => d.SocieteId, s => s.SocieteId.Value);
    }
}
