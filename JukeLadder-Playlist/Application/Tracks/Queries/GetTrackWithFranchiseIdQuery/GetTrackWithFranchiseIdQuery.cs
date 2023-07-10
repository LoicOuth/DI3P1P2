using Application.Tracks.Dtos;

namespace Application.Tracks.Queries.GetTrackWithFranchiseIdQuery;

public record GetTrackWithFranchiseIdQuery(string FranchiseId): IRequest<IEnumerable<TrackDto>>;