namespace CollectManagement.Application.Features.Employes.Queries.GetPagedListEmploye;

public record GetPagedListEmployeResponse(
    List<GetPagedListEmployeDto> employes,
    int total
);
