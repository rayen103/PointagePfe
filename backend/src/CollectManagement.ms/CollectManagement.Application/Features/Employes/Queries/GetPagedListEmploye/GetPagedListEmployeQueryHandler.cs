using CollectManagement.Application.Interfaces.Repositories.Employes;

namespace CollectManagement.Application.Features.Employes.Queries.GetPagedListEmploye;

public class GetPagedListEmployeQueryHandler : IRequestHandler<GetPagedListEmployeQuery, GetPagedListEmployeResponse>
{
    private readonly IEmployeRepository _employeRepository;
    private readonly IMapper _mapper;

    public GetPagedListEmployeQueryHandler(IEmployeRepository employeRepository, IMapper mapper)
    {
        _employeRepository = employeRepository;
        _mapper = mapper;
    }

    public async Task<GetPagedListEmployeResponse> Handle(GetPagedListEmployeQuery request, CancellationToken cancellationToken)
    {
        var (listEmploye, length) = await _employeRepository
            .GetPagedListAsync(
                request.Search,
                request.Sort,
                request.Order,
                request.Page,
                request.Size,
                cancellationToken
            )
            .ConfigureAwait(false);

        return new GetPagedListEmployeResponse(
            _mapper.Map<List<GetPagedListEmployeDto>>(listEmploye),
            length
        );
    }
}
