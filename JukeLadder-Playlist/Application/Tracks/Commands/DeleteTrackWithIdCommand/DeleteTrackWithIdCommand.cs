namespace Application.Tracks.Commands.DeleteTrackWithIdCommand;

public record DeleteTrackWithIdCommand(string TrackId): IRequest;
