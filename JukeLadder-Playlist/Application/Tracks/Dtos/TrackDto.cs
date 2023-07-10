namespace Application.Tracks.Dtos;

public class TrackDto
{
    public string Id { get; set; }
    public string FranchiseId { get; set; }
    public string DeezerId { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Cover { get; set; }
    public int Duration { get; set; }
    public List<string> Upvotes { get; set; }
    public List<string> Downvotes { get; set; }
    public bool IsReading { get; set; }
    public float CurrentDuration { get; set; }
    public DateTime? DatePromote { get; set; }
    public int Position { get; set; }
}
