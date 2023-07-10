using Microsoft.Extensions.Logging;

namespace Application.Tracks.Commands.DeleteTrackWithIdCommand;

public class DeleteTrackWithIdCommandHandler : IRequestHandler<DeleteTrackWithIdCommand>
{
    private readonly IMongoDbHelper<Track> _trackHelper;
    private readonly ILogger<DeleteTrackWithIdCommandHandler> _logger;
    public DeleteTrackWithIdCommandHandler(IMongoDbHelper<Track> trackHelper, ILogger<DeleteTrackWithIdCommandHandler> logger)
    {
        _trackHelper = trackHelper;
        _logger = logger;
    }
    public async Task<Unit> Handle(DeleteTrackWithIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Delete track {id}", request.TrackId);
            var track = await _trackHelper.GetAsync(x => x.Id == request.TrackId, cancellationToken);

            if (track == null)
                throw new NotFoundException("Track", request.TrackId);

            await _trackHelper.DeleteAsync(x => x.Id == request.TrackId, cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while deleting track {id} with error: {ex}", request.TrackId, ex.Message);
            throw;
        }
    }
}
