namespace CollectManagement.Application.Features.Bus.Commands.DeleteBus;

public record DeleteBusCommand(Ulid BusId) : IRequest<Unit>;
