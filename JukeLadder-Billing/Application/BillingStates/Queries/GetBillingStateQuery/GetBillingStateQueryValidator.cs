namespace Application.BillingStates.Queries.GetBillingStateQuery;

public class GetBillingStateQueryValidator : AbstractValidator<GetBillingStateQuery>
{
	public GetBillingStateQueryValidator()
	{
		RuleFor(x => x.FranchiseId).NotEmpty().NotNull();
	}
}
