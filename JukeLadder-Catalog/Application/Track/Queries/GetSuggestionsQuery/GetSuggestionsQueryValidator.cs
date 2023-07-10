namespace Application.Track.Queries.GetSuggestionsQuery;

public class GetSuggestionsQueryValidator : AbstractValidator<GetSuggestionsQuery>
{
    public GetSuggestionsQueryValidator()
    {
        RuleFor(x => x.Query).NotEmpty();
    }
}
