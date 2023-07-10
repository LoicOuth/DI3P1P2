namespace Application.Franchises.Command.CreateFranchiseCommand;

public record CreateFranchiseCommand(string UserId, string Name) : IRequest<string>;
