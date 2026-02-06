using CollectManagement.Application.Interfaces.Repositories.Employes;
using CollectManagement.Domain.Employes;
using CollectManagement.Domain.Employes.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Employes.Commands.CreateEmploye;

public class CreateEmployeCommandHandler : IRequestHandler<CreateEmployeCommand, CreateEmployeResponse>
{
    private readonly IEmployeRepository _employeRepository;
    private readonly IMapper _mapper;

    public CreateEmployeCommandHandler(
        IEmployeRepository employeRepository,
        IMapper mapper)
    {
        _employeRepository = employeRepository;
        _mapper = mapper;
    }

    public async Task<CreateEmployeResponse> Handle(CreateEmployeCommand request, CancellationToken cancellationToken)
    {
        var employeId = new EmployeId(Ulid.NewUlid());
        var societeId = new SocieteId(Ulid.Parse(request.SocieteId));

        var employe = Employe.Create(
            employeId,
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

        await _employeRepository
            .AddAsync(employe, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateEmployeResponse>(employe);
    }
}
