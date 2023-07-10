

namespace Application.Tracks.Commands.UpdateTrackCurrentDurationCommand;

public record UpdateTrackCurrentDurationCommand(string TrackId, float NewDuration) : IRequest;
