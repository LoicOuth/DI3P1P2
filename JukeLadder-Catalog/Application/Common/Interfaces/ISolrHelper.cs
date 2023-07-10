using Application.Track.Dto;

namespace Application.Common.Interfaces;

public interface ISolrHelper
{
    Task<List<TrackSolrDto>> QueryableTracks(string field,string value, string? themeFranchise, string franchiseId, CancellationToken cancellationToken);

    Task<List<SuggestionSolrDto>> GetSuggestions(string query, CancellationToken cancellationToken);
    Task<List<TrackSolrDto>> AddTracks(List<TrackSolrDto> trackSolrs, CancellationToken cancellationToken);
}
