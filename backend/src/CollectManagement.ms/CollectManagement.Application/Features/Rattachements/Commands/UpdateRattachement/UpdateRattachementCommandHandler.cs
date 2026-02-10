using CollectManagement.Application.Interfaces.Repositories.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;

namespace CollectManagement.Application.Features.Rattachements.Commands.UpdateRattachement;

public class UpdateRattachementCommandHandler
    : IRequestHandler<UpdateRattachementCommand, UpdateRattachementResponse>
{
    private readonly IRattachementRepository _rattachementRepository;
    private readonly IMapper _mapper;

    public UpdateRattachementCommandHandler(
        IRattachementRepository rattachementRepository,
        IMapper mapper)
    {
        _rattachementRepository = rattachementRepository;
        _mapper = mapper;
    }

    public async Task<UpdateRattachementResponse> Handle(UpdateRattachementCommand request, CancellationToken cancellationToken)
    {
        var rattachementId = new RattachementId(request.RattachementId);

        var rattachement = await _rattachementRepository
            .GetOneAsync(rattachementId, cancellationToken)
            .ConfigureAwait(false);

        rattachement.Update(
            request.NumeroRattachement,
            request.Exercice,
            request.DateRattachement,
            request.NumeroChantier,
            request.CodeClient,
            request.IsInternal,
            request.Cout,
            request.Type,
            request.Nature,
            request.Responsable,
            request.HeureDebut,
            request.HeureFin,
            request.Emplacement,
            request.Reference,
            request.Status,
            request.DateCloture,
            request.Remarque,
            request.IsActive
        );

        await _rattachementRepository
            .UpdateBulkAsync(rattachement, cancellationToken)
            .ConfigureAwait(false);

        return _mapper.Map<UpdateRattachementResponse>(rattachement);
    }
}
