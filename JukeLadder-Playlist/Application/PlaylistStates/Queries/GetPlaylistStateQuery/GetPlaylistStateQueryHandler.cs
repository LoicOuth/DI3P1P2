using Microsoft.Extensions.Logging;

namespace Application.PlaylistStates.Queries.GetPlaylistStateQuery;

public class GetPlaylistStateQueryHandler : IRequestHandler<GetPlaylistStateQuery, bool>
{
    private readonly IMongoDbHelper<PlaylistState> _mongoDbHelper;
    private readonly ILogger<GetPlaylistStateQueryHandler> _logger;
    public GetPlaylistStateQueryHandler(IMongoDbHelper<PlaylistState> mongoDbHelper, ILogger<GetPlaylistStateQueryHandler> logger)
    {
        _mongoDbHelper = mongoDbHelper;
        _logger = logger;
    }
    public async Task<bool> Handle(GetPlaylistStateQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetPlaylistStateQueryHandler: {franchiseId}", request.FranchiseId);

        var state = await _mongoDbHelper.GetAsync(x => x.FranchiseId == request.FranchiseId, cancellationToken);

        if (state == null)
        {
            _logger.LogInformation("Add State for franchise: {franchiseId}", request.FranchiseId);

            state = new PlaylistState
            {
                Enable = true,
                FranchiseId = request.FranchiseId
            };

            await _mongoDbHelper.InsertAsync(state, cancellationToken);
        }

        return state.Enable;
    }
}
