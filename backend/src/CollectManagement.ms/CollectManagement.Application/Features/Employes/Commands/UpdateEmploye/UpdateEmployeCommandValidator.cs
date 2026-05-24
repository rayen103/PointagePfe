using FluentValidation;

namespace CollectManagement.Application.Features.Employes.Commands.UpdateEmploye;

public class UpdateEmployeCommandValidator : AbstractValidator<UpdateEmployeCommand>
{
    public UpdateEmployeCommandValidator()
    {
        RuleFor(r => r.EmployeId)
            .NotEmpty()
            .NotNull()
            .WithMessage("EmployeId is required.");

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

        RuleFor(r => r.CodeCircuit)
            .NotEmpty()
            .WithMessage("Le circuit est obligatoire.");

        RuleFor(r => r.CodePointCollecte)
            .NotEmpty()
            .WithMessage("Le point de collecte est obligatoire.");
    }
}
