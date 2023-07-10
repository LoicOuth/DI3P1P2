using Microsoft.Extensions.Logging;

namespace Application.Tracks.Commands.VoteTrackCommand;

public class VoteTrackCommandHandler : IRequestHandler<VoteTrackCommand>
{
    private readonly IMongoDbHelper<Track> _trackHelper;
    private readonly ILogger<VoteTrackCommand> _logger;
    public VoteTrackCommandHandler(IMongoDbHelper<Track> trackHelper, ILogger<VoteTrackCommand> logger)
    {
        _trackHelper = trackHelper;
        _logger = logger;
    }
    public async Task<Unit> Handle(VoteTrackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Executing {action} for track {id}", request.Action, request.TrackId);
            var track = await _trackHelper.GetAsync(x => x.Id == request.TrackId, cancellationToken);

            if (track == null)
                throw new NotFoundException(nameof(Track), request.TrackId);

            var isInUpVotes = track.Upvotes.Any(x => x == request.Identifier);
            var isInDownVotes = track.Downvotes.Any(x => x == request.Identifier);

            if (request.Action == "up")
            {
                if(isInUpVotes)
                {
                    track.Upvotes.Remove(request.Identifier);
                }
                else if(isInDownVotes)
                {
                    track.Downvotes.Remove(request.Identifier);
                    track.Upvotes.Add(request.Identifier);
                }
                else
                {
                    track.Upvotes.Add(request.Identifier);
                }
            }
            else if (request.Action == "down")
            {
                if(isInDownVotes)
                {
                    track.Downvotes.Remove(request.Identifier);
                }
                else if(isInUpVotes)
                {
                    track.Upvotes.Remove(request.Identifier);
                    track.Downvotes.Add(request.Identifier);
                }
                else
                {
                    track.Downvotes.Add(request.Identifier);
                }
            }

            await _trackHelper.UpdateAsync(x => x.Id == request.TrackId, track, cancellationToken);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during {action} for track {id}: {error}", request.Action, request.TrackId, ex.Message);
            throw;
        }
    }
}
