namespace Application.Deezer.Queries.SearchPlaylistQuery;

public class SearchPlaylistQueryValidator : AbstractValidator<SearchPlaylistQuery>
{
    public SearchPlaylistQueryValidator()
    {
        RuleFor(x => x.Query).NotEmpty();
    }
}
