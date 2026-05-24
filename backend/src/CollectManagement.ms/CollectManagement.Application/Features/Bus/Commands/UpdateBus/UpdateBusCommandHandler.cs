using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Bus;
using CollectManagement.Application.Interfaces.Repositories.Modems;
using CollectManagement.Domain.Bus.ValueObjects;
using FluentValidation.Results;

namespace CollectManagement.Application.Features.Bus.Commands.UpdateBus;

public class UpdateBusCommandHandler
    : IRequestHandler<UpdateBusCommand, UpdateBusResponse>
{
    private readonly IBusRepository _busRepository;
    private readonly IModemRepository _modemRepository;
    private readonly IMapper _mapper;

    public UpdateBusCommandHandler(
        IBusRepository busRepository,
        IModemRepository modemRepository,
        IMapper mapper)
    {
        _busRepository = busRepository;
        _modemRepository = modemRepository;
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

        return _mapper.Map<UpdateBusResponse>(bus);
    }
}
