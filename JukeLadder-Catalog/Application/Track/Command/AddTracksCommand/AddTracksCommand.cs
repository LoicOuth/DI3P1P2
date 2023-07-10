namespace Application.Track.Command.AddTracksCommand;

public record AddTracksCommand(string IdPlaylist, string IdFranchise) : IRequest;
