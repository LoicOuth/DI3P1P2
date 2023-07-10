using Application.Track.Dto;

namespace Application.Deezer.Queries.SearchPlaylistWithIdQuery;
    public record SearchPlaylistTracksWithIdQuery(string IdPlaylist, string FranchiseId) : IRequest<List<TrackSolrDto>>;

