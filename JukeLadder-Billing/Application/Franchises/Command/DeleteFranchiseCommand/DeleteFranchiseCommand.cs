namespace Application.Franchises.Command.DeleteFranchiseCommand;

public record DeleteFranchiseCommand(string Id) : IRequest;