namespace CollectManagement.Application.Contracts.Authentication;

public record RegisterModeratorRequest(
    string NomUtilisateur,
    string Nom,
    string Prenom,
    string Email);

public record RegisterCompanyRequest(
    string NomSociete,
    string Nom,
    string Prenom,
    string Email,
    string Password);

public record VerifyEmailRequest(
    string Email,
    string Code);

public record VerifyEmailResponse(
    bool Success,
    string Message,
    AuthenticationResponse? Authentication);