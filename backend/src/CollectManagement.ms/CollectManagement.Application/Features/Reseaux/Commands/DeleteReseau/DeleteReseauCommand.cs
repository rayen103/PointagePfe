namespace CollectManagement.Application.Features.Reseaux.Commands.DeleteReseau;

public record DeleteReseauCommand(Ulid ReseauId) : IRequest;
