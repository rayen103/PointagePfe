namespace CollectManagement.Application.Features.PointsCollecte.Queries.GetOnePointCollecte;

public record GetOnePointCollecteQuery(Ulid PointCollecteId) : IRequest<GetOnePointCollecteDto>;
