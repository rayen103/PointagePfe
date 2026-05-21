using FluentValidation;

namespace CollectManagement.Application.Features.Utilisateurs.Queries.Login;

public class LoginQueryValidator
    : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(r => r.Login)
            .NotEmpty()
            .NotNull()
            .WithMessage("Login is required.");
        
        RuleFor(r => r.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password is required.");

        RuleFor(r => r.SocieteId)
            .NotEmpty()
            .WithMessage("Company is required.");

        RuleFor(r => r.NumeroChantier)
            .NotEmpty()
            .NotNull()
            .WithMessage("Site is required.");
    }
}
