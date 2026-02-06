namespace CollectManagement.Application.Features.Employes.Commands.DeleteEmploye;

public record DeleteEmployeCommand(string EmployeId) : IRequest<Unit>;
