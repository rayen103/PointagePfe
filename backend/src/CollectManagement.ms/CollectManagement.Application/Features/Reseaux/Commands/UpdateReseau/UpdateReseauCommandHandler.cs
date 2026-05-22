using CollectManagement.Application.Interfaces.Repositories.Reseaux;
using CollectManagement.Domain.Reseaux.ValueObjects;

namespace CollectManagement.Application.Features.Reseaux.Commands.UpdateReseau;

public class UpdateReseauCommandHandler : IRequestHandler<UpdateReseauCommand, UpdateReseauResponse>
{
    private readonly IReseauRepository _reseauRepository;
    private readonly IMapper _mapper;

    public UpdateReseauCommandHandler(IReseauRepository reseauRepository, IMapper mapper)
    {
        _reseauRepository = reseauRepository;
        _mapper = mapper;
    }

    public async Task<UpdateReseauResponse> Handle(UpdateReseauCommand request, CancellationToken cancellationToken)
    {
        var reseau = await _reseauRepository.GetOneAsync(new ReseauId(request.ReseauId), cancellationToken).ConfigureAwait(false);
        reseau.Update(request.IpAddress, request.Port, request.GmtPlus, request.Latitude, request.Longitude, request.Rayon, request.TimeToleranceMinute, request.IsActive);
        await _reseauRepository.UpdateBulkAsync(reseau, cancellationToken).ConfigureAwait(false);
        return _mapper.Map<UpdateReseauResponse>(reseau);
    }
}
