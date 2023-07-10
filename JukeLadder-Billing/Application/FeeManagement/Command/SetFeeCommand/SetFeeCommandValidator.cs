namespace Application.FeeManagement.Command.SetFeeCommand;

public class SetFeeCommandValidator : AbstractValidator<SetFeeCommand>
{
    public SetFeeCommandValidator(IProductHelper productHelper, IStripeSettings stripeSettings)
    {
        RuleFor(x => x.Min).NotEmpty();
        RuleFor(x => x.Max).NotEmpty();
        RuleFor(x => x).Must(x => x.Max > x.Min).WithMessage("Maximum must be greater than the minimum ");
    }
}

