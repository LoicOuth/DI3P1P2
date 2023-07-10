using Microsoft.Extensions.Logging;

namespace Application.Tracks.Commands.UpdateTrackPositionCommand;
public class UpdateTrackPositionCommandHandler : IRequestHandler<UpdateTrackPositionCommand>
{
    private readonly IMongoDbHelper<Track> _trackMongoHelper;
    private readonly ILogger<UpdateTrackPositionCommand> _logger;
    public UpdateTrackPositionCommandHandler(IMongoDbHelper<Track> trackMongoHelper, ILogger<UpdateTrackPositionCommand> logger)
    {
        _trackMongoHelper = trackMongoHelper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateTrackPositionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Executing update track position for track {id}", request.TrackId);

            var track = await _trackMongoHelper.GetAsync(x => x.Id == request.TrackId, cancellationToken);
            var anyItemPos = await _trackMongoHelper.GetAsync(x => x.Position == request.NewPosition, cancellationToken);

            if (anyItemPos != null)
            {
                anyItemPos.Position++;
                await _trackMongoHelper.UpdateAsync(x => x.Id == anyItemPos.Id, anyItemPos, cancellationToken);
            }

            if (track == null)
                throw new NotFoundException(nameof(Track), request.TrackId);

            track.Position = request.NewPosition;

            await _trackMongoHelper.UpdateAsync(x => x.Id == request.TrackId, track, cancellationToken);

            return Unit.Value;
        }
        catch (Exception)
        {
            _logger.LogInformation("Error during executing update track position for track {id}", request.TrackId);
            throw;
        }
    }
}