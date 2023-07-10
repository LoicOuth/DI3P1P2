using SolrNet.Attributes;

namespace Application.Track.Dto;

public class TrackSolrDto
{
    [SolrUniqueKey("id")]
    public string Id { get; set; }
    [SolrField("franchiseId")]
    public string FranchiseId { get; set; }
    [SolrField("title")]
    public string Title { get; set; }
    [SolrField("artist")]
    public string Artist { get; set; }
    [SolrField("album")]
    public string Album { get; set; }
    [SolrField("cover")]
    public string Cover { get; set; }
    [SolrField("duration")]
    public int Duration { get; set; }
    [SolrField("genre")]
    public string Genre { get; set; }

    public TrackSolrDto(string id, string franchiseId, string title, string artist, string album, string cover, int duration, string genre)
    {
        Id = id;
        FranchiseId = franchiseId;
        Title = title;
        Artist = artist;
        Album = album;
        Cover = cover;
        Duration = duration;
        Genre = genre;
        FranchiseId = franchiseId;
    }

    public TrackSolrDto() { }
}
