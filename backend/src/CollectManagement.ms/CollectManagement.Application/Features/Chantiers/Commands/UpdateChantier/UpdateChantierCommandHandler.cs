using CollectManagement.Application.Interfaces.Repositories.Chantiers;
using CollectManagement.Domain.Chantiers.ValueObjects;

namespace CollectManagement.Application.Features.Chantiers.Commands.UpdateChantier;

public class UpdateChantierCommandHandler
    : IRequestHandler<UpdateChantierCommand, UpdateChantierResponse>
{
    private readonly IChantierRepository _chantierRepository;
    private readonly IMapper _mapper;

    public UpdateChantierCommandHandler(IChantierRepository chantierRepository, IMapper mapper)
    {
        _chantierRepository = chantierRepository;
        _mapper = mapper;
    }

    public async Task<UpdateChantierResponse> Handle(UpdateChantierCommand request, CancellationToken cancellationToken)
    {
        var chantierId = new ChantierId(request.ChantierId);
        var chantier = await _chantierRepository.GetOneAsync(chantierId, cancellationToken).ConfigureAwait(false);

        chantier.Update(
            request.NumeroChantier, request.LibelleChantier, request.CodeClient,
            request.Adresse, request.MontantHT, request.MontantTTC, request.Nature,
            request.Responsable, request.DateDebut, request.DateFin, request.Status, request.IsActive);

        await _chantierRepository.UpdateBulkAsync(chantier, cancellationToken).ConfigureAwait(false);
        return _mapper.Map<UpdateChantierResponse>(chantier);
    }
}
