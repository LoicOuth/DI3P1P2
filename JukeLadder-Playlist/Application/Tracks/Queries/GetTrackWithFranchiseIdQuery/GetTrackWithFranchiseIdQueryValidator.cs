namespace Application.Tracks.Queries.GetTrackWithFranchiseIdQuery;

public class GetTrackWithFranchiseIdQueryValidator : AbstractValidator<GetTrackWithFranchiseIdQuery>
{
	public GetTrackWithFranchiseIdQueryValidator()
	{
        RuleFor(x => x.FranchiseId)
            .NotEmpty();
    }
}
