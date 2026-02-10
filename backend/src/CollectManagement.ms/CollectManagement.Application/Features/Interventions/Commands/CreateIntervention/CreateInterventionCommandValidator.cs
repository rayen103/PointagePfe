using FluentValidation;

namespace CollectManagement.Application.Features.Interventions.Commands.CreateIntervention;

public class CreateInterventionCommandValidator : AbstractValidator<CreateInterventionCommand>
{
    public CreateInterventionCommandValidator()
    {
        RuleFor(r => r.NumeroIntervention)
            .NotEmpty()
            .NotNull()
            .WithMessage("NumeroIntervention is required.");

        RuleFor(r => r.DateIntervention)
            .NotEmpty()
            .WithMessage("DateIntervention is required.");
    }
}
