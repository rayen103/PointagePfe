using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Modems;
using CollectManagement.Application.Interfaces.Repositories.Chauffeurs;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Chauffeurs;
using CollectManagement.Domain.Societes.ValueObjects;
using FluentValidation.Results;

namespace CollectManagement.Application.Features.Bus.Commands.CreateBus;

public class CreateBusCommandHandler
    : IRequestHandler<CreateBusCommand, CreateBusResponse>
{
    private readonly IBusRepository _busRepository;
    private readonly IModemRepository _modemRepository;
    private readonly IChauffeurRepository _chauffeurRepository;
    private readonly IMapper _mapper;

    public CreateBusCommandHandler(
        IBusRepository busRepository,
        IModemRepository modemRepository,
        IChauffeurRepository chauffeurRepository,
        IMapper mapper)
    {
        _busRepository = busRepository;
        _modemRepository = modemRepository;
        _chauffeurRepository = chauffeurRepository;
        _mapper = mapper;
    }

    public async Task<CreateBusResponse> Handle(CreateBusCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.IMEI))
        {
            var modem = await _modemRepository
                .GetAsync(x => x.IMEI == request.IMEI, cancellationToken)
                .ConfigureAwait(false);

            if (modem is null)
            {
                throw new CustomValidationException(new ValidationResult(new[]
                {
                    new ValidationFailure("IMEI", $"Le modem avec l'IMEI {request.IMEI} n'existe pas.")
                }));
            }
        }

        var busId = new BusId(Ulid.NewUlid());
        var societeId = new SocieteId(request.SocieteId);

        var bus = Domain.Bus.Bus.Create(
            busId,
            request.NumeroIMM,
            request.ModelBus,
            request.IMEI,
            request.Capacite,
            request.CodeCircuit,
            request.CodeChauffeur,
            request.AppSagem,
            request.IsActive,
            request.Latitude,
            request.Longitude,
            societeId);

        await _busRepository
            .AddAsync(bus, cancellationToken)
            .ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(request.CodeChauffeur))
        {
            var normalizedCode = request.CodeChauffeur.Trim().ToLower();
            var chauffeur = await _chauffeurRepository
                .GetAsync(x => x.CodeChauffeur.ToLower() == normalizedCode, cancellationToken)
                .ConfigureAwait(false);

            if (chauffeur is not null)
            {
                chauffeur.AssignBus(busId);
                _chauffeurRepository.Update(chauffeur);

                var otherBuses = await _busRepository
                    .GetManyAsync(x => x.CodeChauffeur != null && x.CodeChauffeur.ToLower() == normalizedCode && x.BusId != busId, cancellationToken)
                    .ConfigureAwait(false);

                foreach (var otherBus in otherBuses)
                {
                    otherBus.AssignChauffeur(null);
                    _busRepository.Update(otherBus);
                }
            }
        }

        return _mapper.Map<CreateBusResponse>(bus);
    }
}
