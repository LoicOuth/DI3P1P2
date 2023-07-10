using Application.Helpers;
using Application.Saga;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Tracks.Commands.ForceAddTrackToPlaylistCommand;

public class ForceAddTrackToPlaylistCommand : IConsumer<ForceAddTrackRequest>
{
    private readonly TrackHelper _trackHelper;
    private readonly ILogger<ForceAddTrackToPlaylistCommand> _logger;
    public ForceAddTrackToPlaylistCommand(TrackHelper tracksHelper, ILogger<ForceAddTrackToPlaylistCommand> logger)
    {
        _trackHelper = tracksHelper;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<ForceAddTrackRequest> context)
    {
        try
        {
            ForceAddTrackResponse response;
            _logger.LogInformation("Force add track to playlist for franchise {id}", context.Message.FranchiseId);
            var checkExist = await _trackHelper.CheckIfExist(context.Message.FranchiseId, context.Message.DeezerId, context.CancellationToken);
            if (checkExist)
            {
                response = new ForceAddTrackResponse(true, context.Message.PaymentIntendId)
                {
                    CorrelationId = context.Message.CorrelationId
                };
            }
            else
            {
                response = new ForceAddTrackResponse(false, context.Message.PaymentIntendId)
                {
                    CorrelationId = context.Message.CorrelationId
                };
                await _trackHelper.InsertAsync(context.Message.FranchiseId, context.Message.DeezerId, context.Message.Title, context.Message.Artist, context.Message.Album, context.Message.Cover, context.Message.Duration, DateTime.Now, context.CancellationToken);
            }

            await context.RespondAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during force add track to playlist for franchise {id} with error: {ex}", context.Message.FranchiseId, ex.Message);
            throw;
        }
    }
}
