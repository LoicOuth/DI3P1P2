using Application.Helpers;
using Microsoft.Extensions.Logging;

namespace Application.Tracks.Commands.EndTrackCommand;

public class EndTrackCommandHandler : IRequestHandler<EndTrackCommand>
{
    private readonly IMongoDbHelper<Track> _trackMongoHelper;
    private readonly TrackHelper _trackHelper;
    private readonly ILogger<EndTrackCommand> _logger;
    public EndTrackCommandHandler(TrackHelper trackHelper, IMongoDbHelper<Track> trackMongoHelper, ILogger<EndTrackCommand> logger)
    {
        _trackMongoHelper = trackMongoHelper;
        _trackHelper = trackHelper;
        _logger = logger;
    }
    public async Task<Unit> Handle(EndTrackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Executing end for track {id}", request.TrackId);
            await _trackMongoHelper.DeleteAsync(x => x.Id == request.TrackId, cancellationToken);

            var track = await _trackHelper.GetNextTrack(request.FranchiseId, cancellationToken);
            var tracks = await _trackMongoHelper.GetAll(x=>x.FranchiseId == request.FranchiseId, cancellationToken);

            if (tracks.Any())
            {
                foreach (var item in tracks)
                {
                    if (item.Position != 0)
                    {
                        item.Position--;
                        await _trackMongoHelper.UpdateAsync(x => x.Id == item.Id, item, cancellationToken);
                    }
                }
            }

            if (track == null)
                throw new NotFoundException(nameof(Track), request.TrackId);

            track.IsReading = true;
            track.Position = 0;

            await _trackMongoHelper.UpdateAsync(x => x.Id == track.Id, track, cancellationToken);

            return Unit.Value;

        }   
        catch (Exception ex)
        {
            _logger.LogError("Error during end of track {id}: {error}", request.TrackId, ex.Message);
            throw;
        }
    }
}
