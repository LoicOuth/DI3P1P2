using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Deezer.Dto;
using Application.Track.Dto;
using Newtonsoft.Json;

namespace Infrastructure.Deezer;

public class DeezerPlaylistHelper : IDeezerPlaylistHelper
{
    private readonly HttpClient _httpClient;
    private readonly IDeezerSettings _settings;

    public DeezerPlaylistHelper(IDeezerSettings settings)
    {
        _httpClient = new ();
        _settings = settings;
    }

    public async Task<List<SearchPlaylistDto>> SearchPlaylists(string query)
    {

        HttpResponseMessage response = await _httpClient.GetAsync($"{_settings.DeezerUrl}/search/playlist?q={query}");

        var responseData = JsonConvert.DeserializeObject<ResultDeezer<List<SearchPlaylistDto>>>(await response.Content.ReadAsStringAsync());

        if (responseData == null)
            throw new NotFoundException("Playlist", query);

        return responseData.Data;

    }

    public async Task<List<TrackSolrDto>> SearchTracksPlaylistsWithIdPlaylist(string id, string franchiseId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_settings.DeezerUrl}/playlist/{id}/tracks");

        dynamic responseData = JsonConvert.DeserializeObject<ResultDeezer<dynamic>>(await response.Content.ReadAsStringAsync())!;

        if (responseData == null)
            throw new NotFoundException("Playlist", id);

        List<TrackSolrDto> tracks = new ();

        foreach (var item in responseData.Data)
        {
            string idTrack = item.id;
            string title = item.title;
            string cover = item.artist.picture_medium;
            int duration = item.duration;
            string artist = item.artist.name;
            string album = item.album.title;
            string genre = await SearchGenreWithAlbumId((string)item.album.id);
            tracks.Add(new TrackSolrDto(idTrack, franchiseId, title, artist, album, cover, duration, genre));
        }

        return tracks;
    }

    private async Task<string> SearchGenreWithAlbumId(string id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_settings.DeezerUrl}/album/{id}");

        dynamic responseData = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync())!;

        if (responseData == null)
            throw new NotFoundException("Album", id);

        string genre = responseData.genres.data[0].name;

        return genre;
    }
    
    public async Task<List<string>> SearchAllGenre()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_settings.DeezerUrl}/genre");

        dynamic responseData = JsonConvert.DeserializeObject<ResultDeezer<dynamic>>(await response.Content.ReadAsStringAsync())!;
        
        if (responseData == null)
            throw new NotFoundException("Genre");

        List<string> genres = new();

        foreach (var item in responseData.Data)
        {
            string genre = item.name;
            genres.Add(genre);
        }

        return genres;
    }

}
