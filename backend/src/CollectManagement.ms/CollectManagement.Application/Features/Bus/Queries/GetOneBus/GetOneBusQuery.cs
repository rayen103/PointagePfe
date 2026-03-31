namespace CollectManagement.Application.Features.Bus.Queries.GetOneBus;

public record GetOneBusQuery(Ulid BusId) : IRequest<GetOneBusDto>;
