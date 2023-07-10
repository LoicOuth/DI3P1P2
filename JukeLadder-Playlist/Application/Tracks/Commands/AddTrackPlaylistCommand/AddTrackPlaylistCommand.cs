namespace Application.Tracks.Commands.AddTrackPlaylistCommand;

public record AddTrackPlaylistCommand(string FranchiseId, string DeezerId, string Title, string Artist, string Album, string Cover, int Duration) : IRequest;