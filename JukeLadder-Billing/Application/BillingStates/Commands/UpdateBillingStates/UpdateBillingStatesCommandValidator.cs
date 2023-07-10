namespace Application.BillingStates.Commands.UpdateBillingStates;

public class UpdateBillingStatesCommandValidator : AbstractValidator<UpdateBillingStatesCommand>
{
	public UpdateBillingStatesCommandValidator()
	{
        RuleFor(v => v.FranchiseId).NotNull().NotEmpty();
        RuleFor(v => v.Enable).NotNull();
    }
}
