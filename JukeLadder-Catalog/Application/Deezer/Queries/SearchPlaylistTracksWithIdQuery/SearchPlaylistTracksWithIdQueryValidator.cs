namespace Application.Deezer.Queries.SearchPlaylistWithIdQuery;

public class SearchPlaylistQueryWithIdValidator : AbstractValidator<SearchPlaylistTracksWithIdQuery>
{
    public SearchPlaylistQueryWithIdValidator()
    {
        RuleFor(x => x.IdPlaylist).NotEmpty();
    }
}
