namespace Application.Tracks.Commands.VoteTrackCommand;

public record VoteTrackCommand(string TrackId, string Identifier, string Action) : IRequest;