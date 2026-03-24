namespace CollectManagement.Application.Features.Chantiers.Queries.GetOneChantier;

public record GetOneChantierQuery(Ulid ChantierId) : IRequest<GetOneChantierDto>;
