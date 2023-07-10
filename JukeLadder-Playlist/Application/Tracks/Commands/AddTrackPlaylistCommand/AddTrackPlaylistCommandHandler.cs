using Application.Helpers;
using Microsoft.Extensions.Logging;

namespace Application.Tracks.Commands.AddTrackPlaylistCommand;

public class AddTrackPlaylistCommandHandler : IRequestHandler<AddTrackPlaylistCommand>
{
    private readonly TrackHelper _trackHelper;
    private readonly ILogger<AddTrackPlaylistCommandHandler> _logger;
    public AddTrackPlaylistCommandHandler(TrackHelper trackHelper, ILogger<AddTrackPlaylistCommandHandler> logger)
    {
        _trackHelper = trackHelper;
        _logger = logger;
    }
    public async Task<Unit> Handle(AddTrackPlaylistCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Add Track to franchise {id}", request.FranchiseId);
            var anyTrack = await _trackHelper.CheckAnyTracks(request.FranchiseId, cancellationToken);

            if (!anyTrack)
            {
                await _trackHelper.InsertFirstTracksAsync(request.FranchiseId, request.DeezerId, request.Title, request.Artist, request.Album, request.Cover, request.Duration, null, cancellationToken);
            }
            else
            {
                var checkExist = await _trackHelper.CheckIfExist(request.FranchiseId, request.DeezerId, cancellationToken);
                if (checkExist)
                    throw new AlreadyExistException("Song already exist with deezer id :" + request.DeezerId);

                await _trackHelper.InsertAsync(request.FranchiseId, request.DeezerId, request.Title, request.Artist, request.Album, request.Cover, request.Duration, null, cancellationToken);
            }

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while adding track {name} to franchise {id} with error: {ex}", request.Title, request.FranchiseId, ex.Message);
            throw;
        }
    }
}
