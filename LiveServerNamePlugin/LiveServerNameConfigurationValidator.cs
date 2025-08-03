using FluentValidation;

namespace LiveServerNamePlugin;

public class LiveServerNameConfigurationValidator : AbstractValidator<LiveServerNameConfiguration>
{
    public LiveServerNameConfigurationValidator()
    {
        RuleFor(cfg => cfg.UpdateInterval).NotNull().LessThanOrEqualTo(7200).GreaterThanOrEqualTo(1);
        RuleFor(cfg => cfg.Randomize).NotNull();
        RuleFor(cfg => cfg.ListOfNames).NotNull();
    }
}
