namespace CollectManagement.Application.Features.Employes.Queries.GetPagedListEmploye;

public record GetPagedListEmployeResponse(
    List<GetPagedListEmployeDto> Employes,
    int Total
);
