using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Employes;
using CollectManagement.Domain.Employes.ValueObjects;

namespace CollectManagement.Application.Features.Employes.Queries.GetOneEmploye;

public class GetOneEmployeQueryHandler : IRequestHandler<GetOneEmployeQuery, GetOneEmployeResponse>
{
    private readonly IEmployeRepository _employeRepository;
    private readonly IMapper _mapper;

    public GetOneEmployeQueryHandler(IEmployeRepository employeRepository, IMapper mapper)
    {
        _employeRepository = employeRepository;
        _mapper = mapper;
    }

    public async Task<GetOneEmployeResponse> Handle(GetOneEmployeQuery request, CancellationToken cancellationToken)
    {
        var employeId = new EmployeId(request.EmployeId);

        var employe = await _employeRepository
            .GetOneAsync(employeId, cancellationToken)
            .ConfigureAwait(false) ?? throw new NotFoundException("Employe NotFound.");

        return _mapper.Map<GetOneEmployeResponse>(employe);
    }
}
