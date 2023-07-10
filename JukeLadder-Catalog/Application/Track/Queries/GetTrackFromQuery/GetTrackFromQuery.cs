using Application.Track.Dto;
using Domain.Enums;

namespace Application.Track.Queries.GetTrackFromQuery;

public record GetTrackFromQuery(SolrFields Field, string Value, string Genre, string FranchiseId) : IRequest<List<TrackSolrDto>>;
