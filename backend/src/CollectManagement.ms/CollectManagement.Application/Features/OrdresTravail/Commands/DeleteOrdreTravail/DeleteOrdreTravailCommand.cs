namespace CollectManagement.Application.Features.OrdresTravail.Commands.DeleteOrdreTravail;

public record DeleteOrdreTravailCommand(Ulid OrdreTravailId) : IRequest<Unit>;
