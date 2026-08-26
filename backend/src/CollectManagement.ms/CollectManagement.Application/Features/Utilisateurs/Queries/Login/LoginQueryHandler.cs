using CollectManagement.Application.Common;
using CollectManagement.Application.Contracts.Authentication;
using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Interfaces.Repositories.Chantiers;
using CollectManagement.Application.Interfaces.Authentification;
using CollectManagement.Application.Interfaces.Repositories.Utilisateurs;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Domain.Utilisateurs.ValueObjects;

namespace CollectManagement.Application.Features.Utilisateurs.Queries.Login;

public sealed class LoginQueryHandler
    : IRequestHandler<LoginQuery, AuthenticationResponse>
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IChantierRepository _chantierRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginQueryHandler(
        IUtilisateurRepository utilisateurRepository,
        IChantierRepository chantierRepository,
        IPasswordService passwordService,
        IJwtTokenGenerator tokenGenerator)
    {
        _utilisateurRepository = utilisateurRepository;
        _chantierRepository = chantierRepository;
        _passwordService = passwordService;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthenticationResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var utilisateur = await _utilisateurRepository.TryToLogin(
            request.Login,
            request.SocieteId,
            cancellationToken).ConfigureAwait(false);

        var hasActiveSite = string.IsNullOrWhiteSpace(request.NumeroChantier) ||
                            (utilisateur is not null &&
                             await _chantierRepository.ExistsActiveByNumeroAndSocieteAsync(
                                 request.NumeroChantier,
                                 utilisateur.SocieteId,
                                 cancellationToken).ConfigureAwait(false));
        
        var utilisateurIdForVerification = utilisateur?.UtilisateurId
                                        ?? new UtilisateurId(Ulid.Parse("00000000000000000000000000"));
        var hashedPasswordForVerification = utilisateur?.Password
                                            ?? new string('0', 128);
        var passwordVerificationResult = _passwordService.VerifyHashedPassword(
            utilisateurIdForVerification,
            hashedPasswordForVerification,
            request.Password);

        if (utilisateur is null
            || passwordVerificationResult == PasswordVerificationResult.Failure
            || !hasActiveSite)
            throw new BadCredentialException("Invalid User");

        var token = _tokenGenerator.GenerateToken(utilisateur);

        return new AuthenticationResponse(
            utilisateur.UtilisateurId.Value,
            utilisateur.Nom,
            utilisateur.NomUtilisateur,
            utilisateur.Prenom,
            utilisateur.Email,
            utilisateur.RoleUtilisateur?.Navigations
                .Select(s=> new AuthenticationNavigation(
                    s.NavigationId,
                    s.Actions.Select(a=> (int)a ).ToList(),
                    s.Sections.Select(section=> new AuthenticationSection(
                        section.SectionId,
                        section.Actions.Select(a=> (int)a).ToList()
                    )).ToList()))
                .ToList()??[],
            token,
            utilisateur.SocieteId.Value);
    }
}
