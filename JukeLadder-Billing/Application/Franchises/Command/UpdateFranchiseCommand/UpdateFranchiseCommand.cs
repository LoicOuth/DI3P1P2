namespace Application.Franchises.Command.UpdateFranchiseCommand;

public record UpdateFranchiseCommand(string FranchiseId, string UserId, string Name, string Theme) : IRequest;
