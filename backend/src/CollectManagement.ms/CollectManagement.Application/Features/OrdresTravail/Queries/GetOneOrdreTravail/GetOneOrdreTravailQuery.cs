namespace CollectManagement.Application.Features.OrdresTravail.Queries.GetOneOrdreTravail;

public record GetOneOrdreTravailQuery(Ulid OrdreTravailId) : IRequest<GetOneOrdreTravailDto>;
