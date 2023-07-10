namespace Application.Track.Queries.GetTrackFromQuery;

public class GetTrackFromQueryValidator : AbstractValidator<GetTrackFromQuery>
{
    public GetTrackFromQueryValidator()
    {
        RuleFor(x => x.Field).NotNull();
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.Genre).NotEmpty().NotNull();
        RuleFor(x => x.FranchiseId).NotEmpty().NotNull();
    }
}
