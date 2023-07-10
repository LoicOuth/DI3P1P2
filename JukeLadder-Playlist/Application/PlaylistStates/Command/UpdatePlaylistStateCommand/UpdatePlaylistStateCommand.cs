namespace Application.PlaylistStates.Command.UpdatePlaylistStateCommand;

public record UpdatePlaylistStateCommand(string FranchiseId, bool Enable) : IRequest;
