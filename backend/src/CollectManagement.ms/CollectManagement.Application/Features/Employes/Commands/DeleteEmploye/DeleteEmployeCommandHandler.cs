using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Employes;
using CollectManagement.Domain.Employes.ValueObjects;

namespace CollectManagement.Application.Features.Employes.Commands.DeleteEmploye;

public class DeleteEmployeCommandHandler : IRequestHandler<DeleteEmployeCommand, Unit>
{
    private readonly IEmployeRepository _employeRepository;

    public DeleteEmployeCommandHandler(IEmployeRepository employeRepository)
    {
        _employeRepository = employeRepository;
    }

    public async Task<Unit> Handle(DeleteEmployeCommand request, CancellationToken cancellationToken)
    {
        var employeId = new EmployeId(Ulid.Parse(request.EmployeId));

        var employe = await _employeRepository
            .GetByIdAsync(employeId)
            .ConfigureAwait(false);

        if (employe is null)
        {
            throw new NotFoundException(nameof(employe), request.EmployeId);
        }

        await _employeRepository
            .DeleteAsync(e => e.EmployeId == employeId, cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}
