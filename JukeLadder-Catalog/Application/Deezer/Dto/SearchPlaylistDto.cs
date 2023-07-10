namespace Application.Deezer.Dto;

public class SearchPlaylistDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int NbTracks { get; set; }
    public bool Public { get; set; }
    public string Picture { get; set; }
    public SearchPlaylistDto(long id, string title, string description, int nb_tracks, bool isPublic, string picture)
    {
        Id = id;
        Title = title;
        Description = description;
        NbTracks = nb_tracks;
        Public = isPublic;
        Picture = picture;
    }
}
