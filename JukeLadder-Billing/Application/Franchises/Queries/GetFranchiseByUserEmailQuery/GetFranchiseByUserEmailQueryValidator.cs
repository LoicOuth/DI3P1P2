namespace Application.Franchises.Queries.GetFranchiseByUserEmailQuery;

public class GetFranchiseByUserEmailQueryValidator : AbstractValidator<GetFranchiseByUserEmailQuery>
{
	public GetFranchiseByUserEmailQueryValidator()
	{
		RuleFor(x => x.UserEmail).NotEmpty();
	}
}
