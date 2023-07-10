using Application.Common.Interfaces;
using Application.Track.Dto;
using Microsoft.Extensions.Logging;

namespace Application.Deezer.Queries.SearchPlaylistWithIdQuery;

public class SearchPlaylistTracksWithIdQueryHandler : IRequestHandler<SearchPlaylistTracksWithIdQuery, List<TrackSolrDto>>
{

    private readonly ILogger<SearchPlaylistTracksWithIdQueryHandler> _logger;
    private readonly IDeezerPlaylistHelper _deezerPlaylistHelper;

    public SearchPlaylistTracksWithIdQueryHandler(ILogger<SearchPlaylistTracksWithIdQueryHandler> logger, IDeezerPlaylistHelper deezerPlaylistHelper)
    {
        _logger = logger;
        _deezerPlaylistHelper = deezerPlaylistHelper;
    }

    public async Task<List<TrackSolrDto>> Handle(SearchPlaylistTracksWithIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Search playlist with id : {id}", request.IdPlaylist);
            return await _deezerPlaylistHelper.SearchTracksPlaylistsWithIdPlaylist(request.IdPlaylist, request.FranchiseId);
        }
        catch (Exception e)
        {
            _logger.LogError("Error during search playlist with id {error}", e.Message);
            throw;
        }
    }
}
