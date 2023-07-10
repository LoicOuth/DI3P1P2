using Application.Deezer.Dto;
using Application.Track.Dto;

namespace Application.Common.Interfaces;

public interface IDeezerPlaylistHelper
{
    Task<List<string>> SearchAllGenre();
    Task<List<SearchPlaylistDto>> SearchPlaylists(string query);
    Task<List<TrackSolrDto>> SearchTracksPlaylistsWithIdPlaylist(string id, string franchiseId);
}
