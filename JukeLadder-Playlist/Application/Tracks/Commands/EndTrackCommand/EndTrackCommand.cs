namespace Application.Tracks.Commands.EndTrackCommand;

public record EndTrackCommand(string TrackId, string FranchiseId) : IRequest;
