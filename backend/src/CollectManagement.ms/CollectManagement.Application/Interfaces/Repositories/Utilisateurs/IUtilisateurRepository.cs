using CollectManagement.Domain.Utilisateurs;
using CollectManagement.Domain.Utilisateurs.Enums;
using CollectManagement.Domain.Utilisateurs.ValueObjects;

namespace CollectManagement.Application.Interfaces.Repositories.Utilisateurs;

public interface IUtilisateurRepository : IRepositoryBase<Utilisateur>
{
    
    Task<(IReadOnlyList<Utilisateur>, int)> GetPagedListAsync(
        string? search,
        string? sort,
        string? order,
        int page,
        int size,
        CancellationToken cancellationToken);
    
    Task<Utilisateur?> TryToLogin(string login, CancellationToken cancellationToken);

    Task<Utilisateur?> GetOneAsync(
        UtilisateurId utilisateurId,
        CancellationToken cancellationToken);

    Task<Utilisateur?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task<Utilisateur?> GetByNomUtilisateurAsync(
        string nomUtilisateur,
        CancellationToken cancellationToken);

    Task<Utilisateur?> GetByApprovalTokenAsync(
        string token,
        CancellationToken cancellationToken);

    Task<Utilisateur?> GetByEmailWithDetailsAsync(
        string email,
        CancellationToken cancellationToken);
}
