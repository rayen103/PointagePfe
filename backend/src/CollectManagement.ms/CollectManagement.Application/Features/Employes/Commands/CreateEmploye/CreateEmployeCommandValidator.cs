using FluentValidation;

namespace CollectManagement.Application.Features.Employes.Commands.CreateEmploye;

public class CreateEmployeCommandValidator : AbstractValidator<CreateEmployeCommand>
{
    public CreateEmployeCommandValidator()
    {
        RuleFor(r => r.Matricule)
            .NotEmpty()
            .NotNull()
            .WithMessage("Matricule is required.");

        RuleFor(r => r.Nom)
            .NotEmpty()
            .NotNull()
            .WithMessage("Nom is required.");

        RuleFor(r => r.Prenom)
            .NotEmpty()
            .NotNull()
            .WithMessage("Prenom is required.");

        RuleFor(r => r.SocieteId)
            .NotEmpty()
            .NotNull()
            .WithMessage("SocieteId is required.");
    }
}
