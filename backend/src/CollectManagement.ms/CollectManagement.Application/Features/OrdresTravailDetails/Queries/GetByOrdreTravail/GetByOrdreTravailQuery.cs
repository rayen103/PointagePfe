namespace CollectManagement.Application.Features.OrdresTravailDetails.Queries.GetByOrdreTravail;

public record GetByOrdreTravailQuery(Ulid OrdreTravailId) : IRequest<GetByOrdreTravailResponse>;
