using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Modems;
using CollectManagement.Application.Interfaces.Repositories.Chauffeurs;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Chauffeurs;
using FluentValidation.Results;

namespace CollectManagement.Application.Features.Bus.Commands.UpdateBus;

public class UpdateBusCommandHandler
    : IRequestHandler<UpdateBusCommand, UpdateBusResponse>
{
    private readonly IBusRepository _busRepository;
    private readonly IModemRepository _modemRepository;
    private readonly IChauffeurRepository _chauffeurRepository;
    private readonly IMapper _mapper;

    public UpdateBusCommandHandler(
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

    public async Task<UpdateBusResponse> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
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

        var busId = new BusId(request.BusId);

        var bus = await _busRepository
            .GetOneAsync(busId, cancellationToken)
            .ConfigureAwait(false);

        var oldCodeChauffeur = bus.CodeChauffeur;

        bus.Update(
            request.NumeroIMM,
            request.ModelBus,
            request.IMEI,
            request.Capacite,
            request.CodeCircuit,
            request.CodeChauffeur,
            request.AppSagem,
            request.IsActive,
            request.Latitude,
            request.Longitude
        );

        await _busRepository
            .UpdateBulkAsync(bus, cancellationToken)
            .ConfigureAwait(false);

        if (!string.Equals(oldCodeChauffeur, request.CodeChauffeur, StringComparison.OrdinalIgnoreCase))
        {
            if (!string.IsNullOrWhiteSpace(oldCodeChauffeur))
            {
                var oldNormalizedCode = oldCodeChauffeur.Trim().ToLower();
                var oldChauffeur = await _chauffeurRepository
                    .GetAsync(x => x.CodeChauffeur.ToLower() == oldNormalizedCode && x.BusId == busId, cancellationToken)
                    .ConfigureAwait(false);

                if (oldChauffeur is not null)
                {
                    oldChauffeur.AssignBus(null);
                    _chauffeurRepository.Update(oldChauffeur);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.CodeChauffeur))
            {
                var newNormalizedCode = request.CodeChauffeur.Trim().ToLower();
                var newChauffeur = await _chauffeurRepository
                    .GetAsync(x => x.CodeChauffeur.ToLower() == newNormalizedCode, cancellationToken)
                    .ConfigureAwait(false);

                if (newChauffeur is not null)
                {
                    newChauffeur.AssignBus(busId);
                    _chauffeurRepository.Update(newChauffeur);

                    var otherChauffeurs = await _chauffeurRepository
                        .GetManyAsync(x => x.BusId == busId && x.CodeChauffeur.ToLower() != newNormalizedCode, cancellationToken)
                        .ConfigureAwait(false);

                    foreach (var otherChauffeur in otherChauffeurs)
                    {
                        otherChauffeur.AssignBus(null);
                        _chauffeurRepository.Update(otherChauffeur);
                    }

                    var otherBuses = await _busRepository
                        .GetManyAsync(x => x.CodeChauffeur != null && x.CodeChauffeur.ToLower() == newNormalizedCode && x.BusId != busId, cancellationToken)
                        .ConfigureAwait(false);

                    foreach (var otherBus in otherBuses)
                    {
                        otherBus.AssignChauffeur(null);
                        _busRepository.Update(otherBus);
                    }
                }
            }
        }

        return _mapper.Map<UpdateBusResponse>(bus);
    }
}
