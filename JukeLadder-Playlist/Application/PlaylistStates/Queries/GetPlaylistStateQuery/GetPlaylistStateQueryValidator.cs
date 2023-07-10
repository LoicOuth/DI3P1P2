namespace Application.PlaylistStates.Queries.GetPlaylistStateQuery;

public class GetPlaylistStateQueryValidator : AbstractValidator<GetPlaylistStateQuery>
{
    public GetPlaylistStateQueryValidator()
    {
        RuleFor(x => x.FranchiseId).NotNull().NotEmpty();
    }
}
