using CollectManagement.Application.Interfaces.Repositories.Reseaux;
using CollectManagement.Domain.Reseaux;
using CollectManagement.Domain.Reseaux.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Application.Features.Reseaux.Commands.CreateReseau;

public class CreateReseauCommandHandler : IRequestHandler<CreateReseauCommand, CreateReseauResponse>
{
    private readonly IReseauRepository _reseauRepository;
    private readonly IMapper _mapper;

    public CreateReseauCommandHandler(IReseauRepository reseauRepository, IMapper mapper)
    {
        _reseauRepository = reseauRepository;
        _mapper = mapper;
    }

    public async Task<CreateReseauResponse> Handle(CreateReseauCommand request, CancellationToken cancellationToken)
    {
        var reseauId = new ReseauId(Ulid.NewUlid());
        var reseau = Reseau.Create(reseauId, request.IpAddress, request.Port, request.GmtPlus, request.Latitude, request.Longitude, request.Rayon, request.TimeToleranceMinute, request.IsActive, new SocieteId(request.SocieteId));

        await _reseauRepository.AddAsync(reseau, cancellationToken).ConfigureAwait(false);
        return _mapper.Map<CreateReseauResponse>(reseau);
    }
}
