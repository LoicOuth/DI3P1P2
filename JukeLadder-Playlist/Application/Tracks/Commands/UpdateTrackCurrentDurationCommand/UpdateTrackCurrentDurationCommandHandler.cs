using Microsoft.Extensions.Logging;

namespace Application.Tracks.Commands.UpdateTrackCurrentDurationCommand;

public class UpdateTrackCurrentDurationCommandHandler : IRequestHandler<UpdateTrackCurrentDurationCommand>
{
    private readonly IMongoDbHelper<Track> _trackMongoHelper;
    private readonly ILogger<UpdateTrackCurrentDurationCommand> _logger;
    public UpdateTrackCurrentDurationCommandHandler(IMongoDbHelper<Track> trackMongoHelper, ILogger<UpdateTrackCurrentDurationCommand> logger)
    {
        _trackMongoHelper = trackMongoHelper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateTrackCurrentDurationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Executing update track current duration for track {id}", request.TrackId);

            var track = await _trackMongoHelper.GetAsync(x => x.Id == request.TrackId, cancellationToken);

            if(track == null)
                throw new NotFoundException(nameof(Track), request.TrackId);

            track.CurrentDuration = request.NewDuration;

            await _trackMongoHelper.UpdateAsync(x => x.Id == request.TrackId, track, cancellationToken);

            return Unit.Value;
        }
        catch (Exception)
        {
            _logger.LogInformation("Error during executing update track current duration for track {id}", request.TrackId);
            throw;
        }
    }
}
