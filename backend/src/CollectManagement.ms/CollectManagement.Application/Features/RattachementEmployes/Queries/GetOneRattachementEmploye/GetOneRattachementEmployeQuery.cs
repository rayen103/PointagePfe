namespace CollectManagement.Application.Features.RattachementEmployes.Queries.GetOneRattachementEmploye;

public record GetOneRattachementEmployeQuery(Ulid RattachementEmployeId)
    : IRequest<GetOneRattachementEmployeDto>;
