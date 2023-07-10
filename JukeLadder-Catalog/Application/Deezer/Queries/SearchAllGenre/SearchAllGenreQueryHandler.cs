using Application.Common.Interfaces;
using Application.Deezer.Dto;
using Application.Deezer.Queries.SearchPlaylistQuery;
using Microsoft.Extensions.Logging;

namespace Application.Deezer.Queries.SearchAllGenre;

public class SearchAllGenreQueryHandler : IRequestHandler<SearchAllGenreQuery, List<string>>
{

    private readonly ILogger<SearchAllGenreQueryHandler> _logger;
    private readonly IDeezerPlaylistHelper _deezerPlaylistHelper;

    public SearchAllGenreQueryHandler(ILogger<SearchAllGenreQueryHandler> logger, IDeezerPlaylistHelper deezerPlaylistHelper)
    {
        _logger = logger;
        _deezerPlaylistHelper = deezerPlaylistHelper;
    }
    
    public async Task<List<string>> Handle(SearchAllGenreQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Search all genre");

            return await _deezerPlaylistHelper.SearchAllGenre();
        }
        catch (Exception e)
        {
            _logger.LogError("Error during search playlist with error {error}", e.Message);
            throw;
        }
    }
}
