using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.RattachementEmployes.Commands.CreateRattachementEmploye;

public class CreateRattachementEmployeCommandHandler
    : IRequestHandler<CreateRattachementEmployeCommand, CreateRattachementEmployeResponse>
{
    private readonly IRattachementEmployeRepository _rattachementEmployeRepository;
    private readonly IMapper _mapper;

    public CreateRattachementEmployeCommandHandler(
        IRattachementEmployeRepository rattachementEmployeRepository,
        IMapper mapper)
    {
        _rattachementEmployeRepository = rattachementEmployeRepository;
        _mapper = mapper;
    }

    public async Task<CreateRattachementEmployeResponse> Handle(
        CreateRattachementEmployeCommand request,
        CancellationToken cancellationToken)
    {
        var rattachementEmployeId = new RattachementEmployeId(Ulid.NewUlid());
        var rattachementId = new RattachementId(request.RattachementId);
        var societeId = new SocieteId(request.SocieteId);

        var rattachementEmploye = RattachementEmploye.Create(
            rattachementEmployeId,
            rattachementId,
            request.Matricule,
            request.NomPrenom,
            request.DateDebut,
            request.HeureDebut,
            request.DateFin,
            request.HeureFin,
            request.NombreHeure,
            request.Cout,
            request.CoutGlobal,
            request.TypeRattachement,
            request.IsActive,
            societeId);

        await _rattachementEmployeRepository
            .AddAsync(rattachementEmploye, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<CreateRattachementEmployeResponse>(rattachementEmploye);
    }
}
