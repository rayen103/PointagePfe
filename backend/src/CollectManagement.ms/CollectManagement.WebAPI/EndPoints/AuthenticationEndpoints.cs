using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Contracts.Authentication;
using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Features.Utilisateurs.Queries.Login;
using CollectManagement.Application.Features.Utilisateurs.Queries.LoginCheck;
using CollectManagement.Application.Interfaces.Repositories.Societes;
using CollectManagement.Application.Interfaces.Repositories.Utilisateurs;
using CollectManagement.Application.Interfaces.Authentification;
using CollectManagement.Application.Interfaces.Services;
using CollectManagement.Application.Shared;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;
using CollectManagement.Domain.Utilisateurs;
using CollectManagement.Domain.Utilisateurs.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectManagement.WebAPI.EndPoints;

public class AuthenticationEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroupBuilder = app.MapGroup("cm/authentication");
        
        routeGroupBuilder.MapPost("v1/login", LoginAdmin).AllowAnonymous();
        routeGroupBuilder.MapPost("v1/login-check", LoginAdminCheck).AllowAnonymous();
        routeGroupBuilder.MapPost("v1/register", Register).AllowAnonymous();
        routeGroupBuilder.MapPost("v1/verify-email", VerifyEmail).AllowAnonymous();
        routeGroupBuilder.MapGet("v1/approve", Approve).AllowAnonymous();
        routeGroupBuilder.MapPost("v1/resend-code", ResendCode).AllowAnonymous();
        routeGroupBuilder.MapPost("v99/login", LoginSuperAdmin).AllowAnonymous();
        routeGroupBuilder.MapPost("v99/login-check", LoginSuperAdminCheck).AllowAnonymous();
    }

    public static async Task<IResult> LoginSuperAdmin(
        [FromBody] [Required] LoginRequest loginRequest, 
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new LoginQuery(
            loginRequest.Login, 
            loginRequest.Password,
            loginRequest.SocieteId,
            loginRequest.NumeroChantier);
        
        var authenticationResponse = await sender.Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<AuthenticationResponse>(authenticationResponse));
    }
    
    public static async Task<IResult> LoginAdmin(
        [FromBody] [Required] LoginRequest loginRequest, 
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new LoginQuery(
            loginRequest.Login, 
            loginRequest.Password,
            loginRequest.SocieteId,
            loginRequest.NumeroChantier);
        
        var authenticationResponse = await sender.Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<AuthenticationResponse>(authenticationResponse));
    }
    
    public static async Task<IResult> LoginAdminCheck(
        [FromBody] [Required] LoginRequestCheck loginRequest, 
        ISender sender,
        ILoggedInUserService loggedInUserService,
        CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(loggedInUserService.UserId, out var userId))
        {
            throw new NotFoundException("Utilisateur invalide.");
        }
        
        var command = new LoginCheckQuery(userId);
        
        var authenticationResponse = await sender.Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<AuthenticationResponse>(authenticationResponse));
    }

    private static async Task<IResult> LoginSuperAdminCheck(
        [FromBody] [Required] LoginRequestCheck loginRequest, 
        ISender sender,
        ILoggedInUserService loggedInUserService,
        CancellationToken cancellationToken)
    {
        if (!Ulid.TryParse(loggedInUserService.UserId, out var userId))
        {
            throw new NotFoundException("Utilisateur invalide.");
        }
        
        var command = new LoginCheckQuery(userId);
        
        var authenticationResponse = await sender.Send(command, cancellationToken)
            .ConfigureAwait(false);

        return Results.Ok(new ApiResponse<AuthenticationResponse>(authenticationResponse));
    }

    public static async Task<IResult> Register(
        [FromBody] [Required] RegisterCompanyRequest request,
        ISocieteRepository societeRepository,
        IUtilisateurRepository utilisateurRepository,
        IPasswordService passwordService,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        // 0. Check duplicate email
        var existingEmailUser = await utilisateurRepository.GetAsync(u => u.Email == request.Email, cancellationToken).ConfigureAwait(false);
        if (existingEmailUser is not null)
        {
            return Results.Ok(new ApiResponse<object>(new
            {
                success = false,
                message = "Un compte avec cette adresse e-mail existe déjà."
            }));
        }

        // 1. Create Societe
        var societeId = new SocieteId(Ulid.NewUlid());
        var societe = Societe.Create(
            societeId: societeId,
            logoPath: null,
            nom: request.NomSociete,
            initiales: null,
            tva: null,
            rc: null,
            matriculeFiscal: "PENDING",
            rne: null,
            capital: null,
            dateOverture: DateTime.UtcNow,
            telephone1: null,
            telephone2: null,
            fax1: null,
            fax2: null,
            email: request.Email.Length > 30 ? request.Email.Substring(0, 30) : request.Email,
            adresse: null,
            codePostal: null,
            ville: null,
            pays: null,
            codeSociete: null
        );
        await societeRepository.AddAsync(societe, cancellationToken).ConfigureAwait(false);

        // 2. Create inactive Utilisateur
        var emailPrefix = request.Email.Split('@')[0].Replace(" ", "").ToLower();
        var nomUtilisateur = emailPrefix.Length > 20 ? emailPrefix.Substring(0, 20) : emailPrefix;

        var existingUser = await utilisateurRepository.GetAsync(u => u.NomUtilisateur == nomUtilisateur, cancellationToken).ConfigureAwait(false);
        if (existingUser is not null)
        {
            var suffix = Random.Shared.Next(10, 99).ToString();
            nomUtilisateur = nomUtilisateur.Length > 18 ? nomUtilisateur.Substring(0, 18) + suffix : nomUtilisateur + suffix;
        }

        var utilisateurId = new UtilisateurId(Ulid.NewUlid());
        var approvalToken = Guid.NewGuid().ToString("N");
        var hashedPassword = passwordService.HashPassword(utilisateurId, request.Password);

        var utilisateur = Utilisateur.Create(
            utilisateurId: utilisateurId,
            nomUtilisateur: nomUtilisateur,
            nom: request.Nom,
            prenom: request.Prenom,
            email: request.Email,
            password: hashedPassword,
            roleUtilisateurId: null,
            isActive: false, // Must be approved by Admin
            societeId: societeId
        );
        utilisateur.SetApprovalToken(approvalToken);

        await utilisateurRepository.AddAsync(utilisateur, cancellationToken).ConfigureAwait(false);

        // Save changes
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // 3. Send email to admin with approval link
        var requestScheme = httpContext.Request.Scheme;
        var requestHost = httpContext.Request.Host;
        var approveUrl = $"{requestScheme}://{requestHost}/cm/authentication/v1/approve?token={approvalToken}";

        var subject = $"[PointagePfe] Demande d'inscription: {request.NomSociete}";
        var body = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;">
                <h2 style="color: #1e3a8a; border-bottom: 2px solid #3b82f6; padding-bottom: 10px;">Nouvelle Demande d'Inscription</h2>
                <p>Une nouvelle demande d'inscription d'entreprise a été reçue et nécessite votre approbation.</p>
                
                <table style="width: 100%; border-collapse: collapse; margin: 20px 0;">
                    <tr>
                        <td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #f0f0f0; width: 35%;">Société:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #f0f0f0;">{request.NomSociete}</td>
                    </tr>
                    <tr>
                        <td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #f0f0f0;">Nom:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #f0f0f0;">{request.Nom}</td>
                    </tr>
                    <tr>
                        <td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #f0f0f0;">Prénom:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #f0f0f0;">{request.Prenom}</td>
                    </tr>
                    <tr>
                        <td style="padding: 8px; font-weight: bold; border-bottom: 1px solid #f0f0f0;">Email:</td>
                        <td style="padding: 8px; border-bottom: 1px solid #f0f0f0;">{request.Email}</td>
                    </tr>
                </table>
                
                <div style="text-align: center; margin-top: 30px;">
                    <a href="{approveUrl}" style="background-color: #10b981; color: white; padding: 12px 24px; text-decoration: none; border-radius: 6px; font-weight: bold; font-size: 16px; display: inline-block;">
                        Accepter cette inscription
                    </a>
                </div>
            </div>
            """;

        await emailService.SendAdminNotificationAsync(subject, body, cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<object>(new
        {
            success = true,
            message = "Demande d'inscription enregistrée. Un e-mail de notification a été envoyé à l'administrateur."
        }));
    }

    public static async Task<IResult> Approve(
        [FromQuery] string token,
        IUtilisateurRepository utilisateurRepository,
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Results.Content(GetHtmlResponse("Erreur", "Le jeton d'approbation est manquant.", false), "text/html");
        }

        var utilisateur = await utilisateurRepository.GetAsync(
            u => u.ApprovalToken == token, 
            cancellationToken).ConfigureAwait(false);

        if (utilisateur is null)
        {
            return Results.Content(GetHtmlResponse("Déjà traité", "Cette demande d'inscription a déjà été approuvée ou le lien est invalide.", false), "text/html");
        }

        // Activate user
        utilisateur.Update(
            utilisateur.NomUtilisateur,
            utilisateur.Nom,
            utilisateur.Prenom,
            utilisateur.Email,
            utilisateur.Password,
            utilisateur.RoleUtilisateurId,
            true, // IsActive = true
            utilisateur.SocieteId
        );

        // Generate 6-digit verification code
        var code = Random.Shared.Next(100000, 999999).ToString("D6");
        utilisateur.SetVerificationCode(code);
        utilisateur.ClearApprovalToken(); // One-time use

        utilisateurRepository.Update(utilisateur);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // Send email to the user with the code
        var userSubject = "Votre demande d'inscription a été acceptée";
        var userBody = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;">
                <h2 style="color: #10b981; border-bottom: 2px solid #10b981; padding-bottom: 10px;">Inscription Validée !</h2>
                <p>Bonjour {utilisateur.Prenom},</p>
                <p>Nous avons le plaisir de vous informer que votre demande d'inscription a été validée par l'administrateur.</p>
                <p>Voici votre code de connexion à usage unique pour accéder à votre compte :</p>
                
                <div style="text-align: center; margin: 30px 0;">
                    <span style="font-size: 32px; font-weight: bold; letter-spacing: 5px; color: #1e3a8a; background-color: #f3f4f6; padding: 10px 20px; border-radius: 6px; border: 1px dashed #cbd5e1; display: inline-block;">
                        {code}
                    </span>
                </div>
                
                <p>Saisissez ce code dans la page de vérification ouverte sur votre navigateur.</p>
                <p>Si vous avez fermé la page, vous pouvez y retourner en utilisant votre adresse e-mail lors de la connexion.</p>
            </div>
            """;

        await emailService.SendEmailAsync(utilisateur.Email, userSubject, userBody, cancellationToken).ConfigureAwait(false);

        return Results.Content(GetHtmlResponse("Succès", $"L'inscription pour <strong>{utilisateur.Email}</strong> a été approuvée avec succès. Un e-mail contenant le code d'accès lui a été envoyé.", true), "text/html");
    }

    public static async Task<IResult> VerifyEmail(
        [FromBody] [Required] VerifyEmailRequest request,
        IUtilisateurRepository utilisateurRepository,
        IJwtTokenGenerator tokenGenerator,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var utilisateur = await utilisateurRepository.GetAsync(
            u => u.Email == request.Email, 
            cancellationToken).ConfigureAwait(false);

        if (utilisateur is null)
        {
            return Results.Ok(new ApiResponse<VerifyEmailResponse>("Utilisateur non trouvé.", false, 400));
        }

        if (!utilisateur.IsActive)
        {
            return Results.Ok(new ApiResponse<VerifyEmailResponse>("Votre compte n'a pas encore été activé par l'administrateur.", false, 400));
        }

        if (string.IsNullOrEmpty(utilisateur.VerificationCode) || utilisateur.VerificationCode != request.Code)
        {
            return Results.Ok(new ApiResponse<VerifyEmailResponse>("Code de vérification incorrect.", false, 400));
        }

        // Code matches and account is active!
        // Clear verification code
        utilisateur.ClearVerificationCode();
        utilisateurRepository.Update(utilisateur);
        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // Generate auto-login JWT token
        var token = tokenGenerator.GenerateToken(utilisateur);

        var authResponse = new AuthenticationResponse(
            utilisateur.UtilisateurId.Value,
            utilisateur.Nom,
            utilisateur.NomUtilisateur,
            utilisateur.Prenom,
            utilisateur.Email,
            utilisateur.RoleUtilisateur?.Navigations
                .Select(s => new AuthenticationNavigation(
                    s.NavigationId,
                    s.Actions.Select(a => (int)a).ToList(),
                    s.Sections.Select(section => new AuthenticationSection(
                        section.SectionId,
                        section.Actions.Select(a => (int)a).ToList()
                    )).ToList()))
                .ToList() ?? [],
            token,
            utilisateur.SocieteId.Value);

        var verifyResponse = new VerifyEmailResponse(true, "Adresse e-mail vérifiée avec succès.", authResponse);
        return Results.Ok(new ApiResponse<VerifyEmailResponse>(verifyResponse));
    }

    public static async Task<IResult> ResendCode(
        [FromBody] [Required] VerifyEmailRequest request,
        IUtilisateurRepository utilisateurRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken)
    {
        var utilisateur = await utilisateurRepository.GetAsync(
            u => u.Email == request.Email, 
            cancellationToken).ConfigureAwait(false);

        if (utilisateur is null)
        {
            return Results.Ok(new ApiResponse<object>("Utilisateur non trouvé.", false, 400));
        }

        if (!utilisateur.IsActive)
        {
            return Results.Ok(new ApiResponse<object>("Votre demande d'inscription est toujours en attente d'approbation.", false, 400));
        }

        // Generate new 6-digit code
        var code = Random.Shared.Next(100000, 999999).ToString("D6");
        utilisateur.SetVerificationCode(code);
        utilisateurRepository.Update(utilisateur);

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // Send email to the user with the new code
        var userSubject = "Votre nouveau code de connexion";
        var userBody = $"""
            <div style="font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px;">
                <h2 style="color: #10b981; border-bottom: 2px solid #10b981; padding-bottom: 10px;">Nouveau Code de Connexion</h2>
                <p>Bonjour {utilisateur.Prenom},</p>
                <p>Voici votre nouveau code de connexion à usage unique :</p>
                
                <div style="text-align: center; margin: 30px 0;">
                    <span style="font-size: 32px; font-weight: bold; letter-spacing: 5px; color: #1e3a8a; background-color: #f3f4f6; padding: 10px 20px; border-radius: 6px; border: 1px dashed #cbd5e1; display: inline-block;">
                        {code}
                    </span>
                </div>
                
                <p>Saisissez ce code dans la page de vérification.</p>
            </div>
            """;

        await emailService.SendEmailAsync(utilisateur.Email, userSubject, userBody, cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<object>(new { success = true, message = "Un nouveau code vous a été envoyé par email." }));
    }

    private static string GetHtmlResponse(string title, string message, bool isSuccess)
    {
        var icon = isSuccess ? "✅" : "⚠️";
        var color = isSuccess ? "#10b981" : "#f59e0b";
        return $$"""
            <!DOCTYPE html>
            <html lang="fr">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>{{title}}</title>
                <style>
                    body {
                        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                        background-color: #f3f4f6;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        margin: 0;
                    }
                    .card {
                        background: white;
                        padding: 40px;
                        border-radius: 12px;
                        box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05);
                        text-align: center;
                        max-width: 450px;
                        width: 90%;
                    }
                    .icon {
                        font-size: 48px;
                        margin-bottom: 20px;
                    }
                    h1 {
                        color: #1e293b;
                        margin-top: 0;
                        font-size: 24px;
                    }
                    p {
                        color: #64748b;
                        line-height: 1.6;
                        font-size: 16px;
                    }
                    .footer {
                        margin-top: 30px;
                        font-size: 12px;
                        color: #94a3b8;
                    }
                </style>
            </head>
            <body>
                <div class="card">
                    <div class="icon">{{icon}}</div>
                    <h1 style="color: {{color}}">{{title}}</h1>
                    <p>{{message}}</p>
                    <div class="footer">Canadian System Technology · FR</div>
                </div>
            </body>
            </html>
            """;
    }
}
