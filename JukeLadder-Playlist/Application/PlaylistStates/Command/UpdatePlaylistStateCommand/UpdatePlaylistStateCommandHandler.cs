using Microsoft.Extensions.Logging;

namespace Application.PlaylistStates.Command.UpdatePlaylistStateCommand;

public class UpdatePlaylistStateCommandHandler : IRequestHandler<UpdatePlaylistStateCommand>
{
    private readonly IMongoDbHelper<PlaylistState> _mongoDbHelper;
    private readonly ILogger<UpdatePlaylistStateCommandHandler> _logger;
    public UpdatePlaylistStateCommandHandler(IMongoDbHelper<PlaylistState> mongoDbHelper, ILogger<UpdatePlaylistStateCommandHandler> logger)
    {
        _mongoDbHelper = mongoDbHelper;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdatePlaylistStateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Update PlaylistState for franchise {id}", request.FranchiseId);
            var state = await _mongoDbHelper.GetAsync(x => x.FranchiseId == request.FranchiseId, cancellationToken);

            if (state == null)
            {
                _logger.LogInformation("Add State for franchise: {franchiseId}", request.FranchiseId);

                state = new PlaylistState
                {
                    FranchiseId = request.FranchiseId,
                    Enable = request.Enable
                };
                await _mongoDbHelper.InsertAsync(state, cancellationToken);
            }
            else
            {
                state.Enable = request.Enable;
                await _mongoDbHelper.UpdateAsync(x => x.Id == state.Id, state, cancellationToken);
            }
            
            _logger.LogInformation("Successfuly updated PlaylistState for franchise {id}", request.FranchiseId);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during update PlaylistState with error {error}", ex.Message);
            throw;
        }
    }
}
