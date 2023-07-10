namespace Application.Deezer.Dto;

public class TrackDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Cover { get; set; }
    public TrackDto(long id, string title, string artist, string album, string cover, int duration)
    {
        Id = id;
        Title = title;
        Duration = duration;
        Album = album;
        Cover = cover;
        Artist = artist;
    }
}

