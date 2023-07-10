using Application.Common.Interfaces;
using Application.Deezer.Dto;
using Microsoft.Extensions.Logging;

namespace Application.Deezer.Queries.SearchPlaylistQuery;

public class SearchPlaylistQueryHandler : IRequestHandler<SearchPlaylistQuery, List<SearchPlaylistDto>>
{

    private readonly ILogger<SearchPlaylistQueryHandler> _logger;
    private readonly IDeezerPlaylistHelper _deezerPlaylistHelper;

    public SearchPlaylistQueryHandler(ILogger<SearchPlaylistQueryHandler> logger, IDeezerPlaylistHelper deezerPlaylistHelper)
    {
        _logger = logger;
        _deezerPlaylistHelper = deezerPlaylistHelper;
    }

    public async Task<List<SearchPlaylistDto>> Handle(SearchPlaylistQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Search playlist with query: {query}", request.Query);

            return await _deezerPlaylistHelper.SearchPlaylists(request.Query);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during search playlist with error {error}", e.Message);
            throw;
        }
    }
}
