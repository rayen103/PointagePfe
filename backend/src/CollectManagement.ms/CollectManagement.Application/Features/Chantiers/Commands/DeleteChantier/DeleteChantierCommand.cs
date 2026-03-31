namespace CollectManagement.Application.Features.Chantiers.Commands.DeleteChantier;

public record DeleteChantierCommand(Ulid ChantierId) : IRequest<Unit>;
