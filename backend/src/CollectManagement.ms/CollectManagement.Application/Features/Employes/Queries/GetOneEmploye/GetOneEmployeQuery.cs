namespace CollectManagement.Application.Features.Employes.Queries.GetOneEmploye;

public record GetOneEmployeQuery(
    Ulid EmployeId
) : IRequest<GetOneEmployeResponse>;
