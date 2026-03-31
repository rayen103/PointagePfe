namespace CollectManagement.Application.Features.OrdresTravailDetails.Commands.DeleteOrdreTravailDetail;

public record DeleteOrdreTravailDetailCommand(Ulid OrdreTravailDetailId) : IRequest<Unit>;
