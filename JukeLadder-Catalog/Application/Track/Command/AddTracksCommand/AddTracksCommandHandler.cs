using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Track.Command.AddTracksCommand;
public class AddTracksCommandHandler : IRequestHandler<AddTracksCommand>
{
    private readonly ILogger<AddTracksCommandHandler> _logger;
    private readonly ISolrHelper _solrTrackHelper;
    private readonly IDeezerPlaylistHelper _deezerHelper;

    public AddTracksCommandHandler(ILogger<AddTracksCommandHandler> logger, ISolrHelper solrTrackHelper, IDeezerPlaylistHelper deezerHelper)
    {
        _logger = logger;
        _solrTrackHelper = solrTrackHelper;
        _deezerHelper = deezerHelper;
    }

    public async Task<Unit> Handle(AddTracksCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Adding tracks in catalog from deezer playlist with id {id} for franchise {franchiseId}", request.IdPlaylist, request.IdFranchise);
            var tracks = await _deezerHelper.SearchTracksPlaylistsWithIdPlaylist(request.IdPlaylist, request.IdFranchise);

            await _solrTrackHelper.AddTracks(tracks, cancellationToken);

            _logger.LogInformation("Catalogue add tracks of playlist with id {id} tracks", request.IdPlaylist);

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogError("Error during Add tracks on catalogue with erreor {error}", e.Message);
            throw;
        }
    }
}

