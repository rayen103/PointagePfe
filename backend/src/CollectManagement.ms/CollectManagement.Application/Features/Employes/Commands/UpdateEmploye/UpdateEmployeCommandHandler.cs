using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Employes;
using CollectManagement.Domain.Employes.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Employes.Commands.UpdateEmploye;

public class UpdateEmployeCommandHandler : IRequestHandler<UpdateEmployeCommand, Unit>
{
    private readonly IEmployeRepository _employeRepository;

    public UpdateEmployeCommandHandler(IEmployeRepository employeRepository)
    {
        _employeRepository = employeRepository;
    }

    public async Task<Unit> Handle(UpdateEmployeCommand request, CancellationToken cancellationToken)
    {
        var employeId = new EmployeId(Ulid.Parse(request.EmployeId));
        var societeId = new SocieteId(Ulid.Parse(request.SocieteId));

        var employe = await _employeRepository
            .GetByIdAsync(employeId)
            .ConfigureAwait(false);

        if (employe is null)
        {
            throw new NotFoundException(nameof(employe), request.EmployeId);
        }

        employe.Update(
            request.Matricule,
            request.RFID,
            request.Nom,
            request.Prenom,
            request.CodeCircuit,
            request.CodePointCollecte,
            request.CodeShift,
            request.Adresse,
            request.CodeGouvernorat,
            request.CodeRegion,
            societeId
        );

        _employeRepository.Update(employe);

        return Unit.Value;
    }
}
