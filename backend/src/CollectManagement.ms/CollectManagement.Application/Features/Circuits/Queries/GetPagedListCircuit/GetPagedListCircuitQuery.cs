namespace CollectManagement.Application.Features.Circuits.Queries.GetPagedListCircuit;

public record GetPagedListCircuitQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListCircuitResponse>;
