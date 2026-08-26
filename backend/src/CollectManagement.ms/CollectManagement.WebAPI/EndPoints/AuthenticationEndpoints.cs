using System.ComponentModel.DataAnnotations;
using Carter;
using CollectManagement.Application.Common;
using CollectManagement.Application.Contracts.Authentication;
using CollectManagement.Application.Exceptions;
using CollectManagement.Application.Features.Utilisateurs.Queries.Login;
using CollectManagement.Application.Features.Utilisateurs.Queries.LoginCheck;
using CollectManagement.Application.Interfaces.Services;
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
        IEmailService emailService,
        CancellationToken cancellationToken)
    {
        var subject = $"[PointagePfe] Demande d'inscription: {request.NomSociete}";
        var body = $"""
            <h3>Nouvelle Demande d'Inscription Reçue</h3>
            <p><strong>Société:</strong> {request.NomSociete}</p>
            <p><strong>Nom:</strong> {request.Nom}</p>
            <p><strong>Prénom:</strong> {request.Prenom}</p>
            <p><strong>Email:</strong> {request.Email}</p>
            <p><em>Veuillez vérifier cette demande et approuver le compte utilisateur.</em></p>
            """;

        await emailService.SendAdminNotificationAsync(subject, body, cancellationToken).ConfigureAwait(false);

        return Results.Ok(new ApiResponse<object>(new
        {
            success = true,
            message = "Demande d'inscription enregistrée. Un e-mail de notification a été envoyé à l'administrateur."
        }));
    }

    public static Task<IResult> VerifyEmail(
        [FromBody] [Required] VerifyEmailRequest request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IResult>(Results.Ok(new ApiResponse<object>(new
        {
            success = true,
            message = "Adresse e-mail vérifiée avec succès."
        })));
    }
}
