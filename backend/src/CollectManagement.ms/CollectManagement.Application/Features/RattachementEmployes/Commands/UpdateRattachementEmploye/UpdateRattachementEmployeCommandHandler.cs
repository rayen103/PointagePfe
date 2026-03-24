using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.RattachementEmployes.Commands.UpdateRattachementEmploye;

public class UpdateRattachementEmployeCommandHandler
    : IRequestHandler<UpdateRattachementEmployeCommand, UpdateRattachementEmployeResponse>
{
    private readonly IRattachementEmployeRepository _rattachementEmployeRepository;
    private readonly IMapper _mapper;

    public UpdateRattachementEmployeCommandHandler(
        IRattachementEmployeRepository rattachementEmployeRepository,
        IMapper mapper)
    {
        _rattachementEmployeRepository = rattachementEmployeRepository;
        _mapper = mapper;
    }

    public async Task<UpdateRattachementEmployeResponse> Handle(
        UpdateRattachementEmployeCommand request,
        CancellationToken cancellationToken)
    {
        var rattachementEmployeId = new RattachementEmployeId(request.RattachementEmployeId);
        var rattachementId = new RattachementId(request.RattachementId);

        var rattachementEmploye = await _rattachementEmployeRepository
            .GetOneAsync(rattachementEmployeId, cancellationToken)
            .ConfigureAwait(false);

        rattachementEmploye.Update(
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
            request.IsActive);

        await _rattachementEmployeRepository
            .UpdateBulkAsync(rattachementEmploye, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateRattachementEmployeResponse>(rattachementEmploye);
    }
}
