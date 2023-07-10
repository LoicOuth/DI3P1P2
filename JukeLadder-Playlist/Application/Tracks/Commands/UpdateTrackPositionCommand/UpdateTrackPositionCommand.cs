namespace Application.Tracks.Commands.UpdateTrackPositionCommand;
public record class UpdateTrackPositionCommand(int NewPosition, string TrackId) : IRequest;
