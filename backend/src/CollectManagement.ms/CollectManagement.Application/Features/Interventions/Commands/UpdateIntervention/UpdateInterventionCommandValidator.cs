using FluentValidation;

namespace CollectManagement.Application.Features.Interventions.Commands.UpdateIntervention;

public class UpdateInterventionCommandValidator : AbstractValidator<UpdateInterventionCommand>
{
    public UpdateInterventionCommandValidator()
    {
        RuleFor(r => r.InterventionId)
            .NotEmpty()
            .NotNull()
            .WithMessage("InterventionId is required.");

        RuleFor(r => r.NumeroIntervention)
            .NotEmpty()
            .NotNull()
            .WithMessage("NumeroIntervention is required.");

        RuleFor(r => r.DateIntervention)
            .NotEmpty()
            .WithMessage("DateIntervention is required.");
    }
}
