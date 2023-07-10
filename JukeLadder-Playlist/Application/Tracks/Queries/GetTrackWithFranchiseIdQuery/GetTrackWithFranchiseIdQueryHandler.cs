using Application.Helpers;
using Application.Tracks.Dtos;

namespace Application.Tracks.Queries.GetTrackWithFranchiseIdQuery;

public class GetTrackWithFranchiseIdQueryHandler : IRequestHandler<GetTrackWithFranchiseIdQuery, IEnumerable<TrackDto>>
{
    public readonly TrackHelper _trackHelper;
    public GetTrackWithFranchiseIdQueryHandler(TrackHelper trackHelper)
    {
        _trackHelper = trackHelper;
    }
    
    public async Task<IEnumerable<TrackDto>> Handle(GetTrackWithFranchiseIdQuery request, CancellationToken cancellationToken)
    {
        return await _trackHelper.GetTrackWithFranchiseId(request.FranchiseId, cancellationToken);
    }
}
