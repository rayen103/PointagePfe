using FluentValidation;

namespace CollectManagement.Application.Features.Utilisateurs.Commands.UpdateUtilisateur;

public class UpdateUtilisateurCommandValidator : AbstractValidator<UpdateUtilisateurCommand>
{
    public UpdateUtilisateurCommandValidator()
    {
        RuleFor(r => r.Password)
            .Must(password => string.IsNullOrWhiteSpace(password)
                              || System.Text.RegularExpressions.Regex.IsMatch(
                                  password,
                                  @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$"))
            .WithMessage("Password must contain at least 8 characters, with uppercase, lowercase, number and special character.");
    }
}
