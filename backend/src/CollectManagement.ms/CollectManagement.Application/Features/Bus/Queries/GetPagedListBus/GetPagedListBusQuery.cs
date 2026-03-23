namespace CollectManagement.Application.Features.Bus.Queries.GetPagedListBus;

public record GetPagedListBusQuery(
    string? Search,
    string? Sort,
    string? Order,
    int Page,
    int Size
) : IRequest<GetPagedListBusResponse>;
