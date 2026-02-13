namespace CollectManagement.Application.Features.PointsCollecte.Commands.DeletePointCollecte;

public record DeletePointCollecteCommand(Ulid PointCollecteId) : IRequest<Unit>;
