namespace Presentation.Requests
{
    public record VoteTrackLikeRequest(string TrackId, string Identifier, string Action);

    public record AddTrackPlaylistRequest(string Id, string FranchiseId, string Title, string Artist, string Album, string Cover, int Duration);

    public record EndTrackRequest(string FranchiseId); 

    public record UpdateDurationRequest(float NewDuration);
    public record UpdatePositionRequest(int NewPosition);
}
