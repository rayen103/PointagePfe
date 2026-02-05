namespace CollectManagement.Application.Features.Employes.Queries.GetPagedListEmploye;

public record GetPagedListEmployeQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListEmployeResponse>;
